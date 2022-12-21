using Confluent.Kafka;
using DaJet.Agent.Service;
using DaJet.Data.Messaging;
using DaJet.Logging;
using DaJet.ProtoBuf;
using System;
using System.Text;
using System.Threading;
using V1 = DaJet.Data.Messaging.V1;
using V10 = DaJet.Data.Messaging.V10;
using V11 = DaJet.Data.Messaging.V11;
using V12 = DaJet.Data.Messaging.V12;

namespace DaJet.Agent.Kafka.Producer
{
    internal sealed class KafkaMessageProducer : IDisposable
    {
        private int _produced;
        private string _topic;
        private string _error;
        private ProducerConfig _options;
        private IProducer<Null, byte[]> _producer;
        private readonly KafkaProducerSettings _settings;
        private readonly Message<Null, byte[]> _message;
        private readonly Action<IProducer<Null, byte[]>, Error> _errorHandler;
        private readonly Action<IProducer<Null, byte[]>, LogMessage> _logHandler;
        private readonly Action<DeliveryReport<Null, byte[]>> _deliveryReportHandler;
        internal KafkaMessageProducer(KafkaProducerSettings settings)
        {
            _settings = settings;
            _options = new ProducerConfig(_settings.Options);
            _message = new Message<Null, byte[]>();
            _logHandler = new Action<IProducer<Null, byte[]>, LogMessage>(LogHandler);
            _errorHandler = new Action<IProducer<Null, byte[]>, Error>(ErrorHandler);
            _deliveryReportHandler = new Action<DeliveryReport<Null, byte[]>>(HandleDeliveryReport);
        }
        internal int Publish(IMessageConsumer consumer)
        {
            ConfigureProducer();

            _ = Interlocked.Exchange(ref _produced, 0);

            do
            {
                consumer.TxBegin();

                foreach (OutgoingMessageDataMapper message in consumer.Select())
                {
                    ConfigureTopic(in message);
                    ConfigureHeaders(in message);
                    FormatMessageBody(in message);

                    _message.Timestamp = new Timestamp(DateTime.Now);

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
        private void LogHandler(IProducer<Null, byte[]> _, LogMessage message)
        {
            FileLogger.Log($"[Kafka] {message.Message}");
        }
        private void ErrorHandler(IProducer<Null, byte[]> _, Error error)
        {
            FileLogger.Log($"[Kafka] ERROR {error.Reason}");
        }
        private void HandleDeliveryReport(DeliveryReport<Null, byte[]> report)
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

            _message.Value = null;
            _message.Headers = null;

            if (_error != null)
            {
                string error = _error;

                _error = null;

                throw new InvalidOperationException($"[Kafka] ERROR {error}");
            }

            _error = null;
        }
        void IDisposable.Dispose()
        {
            _producer?.Dispose();
            _producer = null;
            _error = null;
            _produced = 0;
            _message.Value = null;
            _message.Headers = null;
        }

        private void ConfigureProducer()
        {
            if (_producer == null)
            {
                _producer = new ProducerBuilder<Null, byte[]>(_options)
                    .SetLogHandler(_logHandler)
                    .SetErrorHandler(_errorHandler)
                    .Build();
            }
        }
        private void ConfigureTopic(in OutgoingMessageDataMapper message)
        {
            if (!_settings.Topics.TryGetValue(message.MessageType, out _topic))
            {
                if (string.IsNullOrWhiteSpace(_settings.DefaultTopic))
                {
                    throw new InvalidOperationException($"[Kafka] Producer topic is not defined for message type \"{message.MessageType}\".");
                }

                _topic = _settings.DefaultTopic;
            }
        }
        private void ConfigureHeaders(in OutgoingMessageDataMapper message)
        {
            Headers headers = new Headers();

            if (message is V1.OutgoingMessage message1)
            {
                ConfigureHeaders(in message1, in headers);
            }
            else if (message is V10.OutgoingMessage message10)
            {
                ConfigureHeaders(in message10, in headers);
            }
            else if (message is V11.OutgoingMessage message11)
            {
                ConfigureHeaders(in message11, in headers);
            }
            else if (message is V12.OutgoingMessage message12)
            {
                ConfigureHeaders(in message12, in headers);
            }

            _message.Headers = headers;
        }
        private void ConfigureHeaders(in V1.OutgoingMessage message, in Headers headers)
        {
            headers.Add("headers", Encoding.UTF8.GetBytes(message.Headers));
            headers.Add("message.id", Encoding.UTF8.GetBytes(message.Uuid.ToString().ToLower()));
            headers.Add("message.type", Encoding.UTF8.GetBytes(message.MessageType));
        }
        private void ConfigureHeaders(in V10.OutgoingMessage message, in Headers headers)
        {
            headers.Add("sender", Encoding.UTF8.GetBytes(message.Sender));
            headers.Add("recipients", Encoding.UTF8.GetBytes(message.Recipients));
            headers.Add("message.id", Encoding.UTF8.GetBytes(message.Uuid.ToString().ToLower()));
            headers.Add("message.type", Encoding.UTF8.GetBytes(message.MessageType));
        }
        private void ConfigureHeaders(in V11.OutgoingMessage message, in Headers headers)
        {
            headers.Add("sender", Encoding.UTF8.GetBytes(message.Sender));
            headers.Add("recipients", Encoding.UTF8.GetBytes(message.Recipients));
            headers.Add("message.id", Encoding.UTF8.GetBytes(message.Uuid.ToString().ToLower()));
            headers.Add("message.type", Encoding.UTF8.GetBytes(message.MessageType));
        }
        private void ConfigureHeaders(in V12.OutgoingMessage message, in Headers headers)
        {
            headers.Add("sender", Encoding.UTF8.GetBytes(message.Sender));
            headers.Add("recipients", Encoding.UTF8.GetBytes(message.Recipients));
            headers.Add("message.id", Encoding.UTF8.GetBytes(message.Uuid.ToString().ToLower()));
            headers.Add("message.type", Encoding.UTF8.GetBytes(message.MessageType));
        }
        private void FormatMessageBody(in OutgoingMessageDataMapper message)
        {
            if (_settings.EntityModel == null)
            {
                _message.Value = Encoding.UTF8.GetBytes(message.MessageBody);
            }
            else
            {
                _message.Value = ProtobufConverter.ConvertJsonToProtobuf(
                    _settings.EntityModel,
                    message.MessageType,
                    message.MessageBody);
            }
        }
    }
}