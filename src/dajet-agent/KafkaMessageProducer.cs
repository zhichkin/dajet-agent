using Confluent.Kafka;
using DaJet.Agent.Service;
using DaJet.Data.Messaging;
using DaJet.Logging;
using System;
using System.Threading;

namespace DaJet.Agent.Kafka.Producer
{
    internal sealed class KafkaMessageProducer : IDisposable
    {
        private int _produced;
        private string _topic;
        private string _error;
        private ProducerConfig _options;
        private IProducer<Null, string> _producer;
        private readonly KafkaProducerSettings _settings;
        private readonly Message<Null, string> _message;
        private readonly Action<IProducer<Null, string>, Error> _errorHandler;
        private readonly Action<IProducer<Null, string>, LogMessage> _logHandler;
        private readonly Action<DeliveryReport<Null, string>> _deliveryReportHandler;
        internal KafkaMessageProducer(KafkaProducerSettings settings)
        {
            _settings = settings;
            _options = new ProducerConfig(_settings.Options);
            _message = new Message<Null, string>();
            _logHandler = new Action<IProducer<Null, string>, LogMessage>(LogHandler);
            _errorHandler = new Action<IProducer<Null, string>, Error>(ErrorHandler);
            _deliveryReportHandler = new Action<DeliveryReport<Null, string>>(HandleDeliveryReport);
        }
        internal int Publish(IMessageConsumer consumer)
        {
            if (_producer == null)
            {
                _producer = new ProducerBuilder<Null, string>(_options)
                    .SetLogHandler(_logHandler)
                    .SetErrorHandler(_errorHandler)
                    .Build();
            }

            _ = Interlocked.Exchange(ref _produced, 0);

            do
            {
                consumer.TxBegin();

                foreach (OutgoingMessageDataMapper message in consumer.Select())
                {
                    if (!_settings.Topics.TryGetValue(message.MessageType, out _topic))
                    {
                        throw new InvalidOperationException($"[Kafka] Producer topic is not found for message type \"{message.MessageType}\".");
                    }

                    //_message.Headers = input.Headers;
                    _message.Value = message.MessageBody;

                    _producer.Produce(_topic, _message, _deliveryReportHandler);
                }

                if (consumer.RecordsAffected > 0)
                {
                    Synchronize();
                }

                consumer.TxCommit();
            }
            while (consumer.RecordsAffected > 0);

            return Interlocked.Exchange(ref _produced, 0);
        }
        private void LogHandler(IProducer<Null, string> _, LogMessage message)
        {
            FileLogger.Log($"[Kafka] {message.Message}");
        }
        private void ErrorHandler(IProducer<Null, string> producer, Error error)
        {
            FileLogger.Log($"[Kafka] ERROR {error.Reason}");
        }
        private void HandleDeliveryReport(DeliveryReport<Null, string> report)
        {
            if (report.Status == PersistenceStatus.Persisted)
            {
                Interlocked.Increment(ref _produced);
            }
            else
            {
                if (report.Error != null && string.IsNullOrWhiteSpace(_error))
                {
                    _error = report.Error.Reason;
                }
            }
        }
        private void Synchronize()
        {
            _producer?.Flush();

            _message.Value = null!;
            _message.Headers = null;

            if (_error != null)
            {
                string error = _error;

                _error = null!;

                throw new InvalidOperationException($"[Kafka] ERROR {error}");
            }

            _error = null!;
        }
        void IDisposable.Dispose()
        {
            _producer?.Dispose();
            _producer = null!;
            _error = null!;
            _produced = 0;
            _message.Value = null!;
            _message.Headers = null;
        }
    }
}