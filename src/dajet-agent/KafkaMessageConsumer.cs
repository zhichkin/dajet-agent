using Confluent.Kafka;
using DaJet.Agent.Service;
using DaJet.Data.Messaging;
using DaJet.Logging;
using DaJet.Metadata;
using DaJet.Metadata.Model;
using System;
using System.Text;
using System.Threading;
using V1 = DaJet.Data.Messaging.V1;
using V10 = DaJet.Data.Messaging.V10;
using V11 = DaJet.Data.Messaging.V11;
using V12 = DaJet.Data.Messaging.V12;

namespace DaJet.Agent.Kafka.Consumer
{
    internal sealed class KafkaMessageConsumer : IDisposable
    {
        private const string DATABASE_INTERFACE_IS_NOT_SUPPORTED_ERROR = "[Kafka] Интерфейс данных входящей очереди не поддерживается.";

        private CancellationToken _token;
        
        private int _version;
        private int _yearOffset = 0;
        private string _metadataName;
        private ApplicationObject _queue;
        private string _connectionString;
        private DatabaseProvider _provider;

        private string _topic;
        private int _consumed = 0;
        private readonly KafkaConsumerSettings _settings;
        private ConsumerConfig _options = new ConsumerConfig();
        private IConsumer<Ignore, byte[]> _consumer;
        private ConsumeResult<Ignore, byte[]> _result;
        private readonly Action<IConsumer<Ignore, byte[]>, Error> _errorHandler;
        private readonly Action<IConsumer<Ignore, byte[]>, LogMessage> _logHandler;
        public KafkaMessageConsumer(KafkaConsumerSettings settings)
        {
            _settings = settings;

            _metadataName = _settings.IncomingQueueName;
            _connectionString = _settings.ConnectionString;
            _provider = GetDatabaseProviderFromConnectionString(in _connectionString);

            _topic = settings.Topic;
            _options = new ConsumerConfig(_settings.Options);
            _errorHandler = new Action<IConsumer<Ignore, byte[]>, Error>(ErrorHandler);
            _logHandler = new Action<IConsumer<Ignore, byte[]>, LogMessage>(LogHandler);
        }
        public void Consume(CancellationToken token)
        {
            _token = token;
            _consumed = 0;

            InitializeMetadata();

            ConfigureConsumer();

            using (IMessageProducer producer = GetMessageProducer())
            {
                do
                {
                    _result = _consumer.Consume(TimeSpan.FromSeconds(1));

                    if (_result != null && _result.Message != null)
                    {
                        IncomingMessageDataMapper message = ProduceMessage(_result.Message);

                        producer.Insert(in message);
                        
                        _ = _consumer.Commit(); // commit consumer offsets

                        _consumed++;
                    }
                }
                while (_result != null && _result.Message != null && !_token.IsCancellationRequested);
            }

            FileLogger.Log($"[Kafka] Consumed {_consumed} messages.");

            try
            {
                _consumer?.Unsubscribe();
            }
            catch (Exception error)
            {
                FileLogger.Log($"[Kafka] ERROR {ExceptionHelper.GetErrorText(error)}");
            }
        }
        private void LogHandler(IConsumer<Ignore, byte[]> _, LogMessage message)
        {
            FileLogger.Log($"[Kafka] [{message.Name}]: " + message.Message);
        }
        private void ErrorHandler(IConsumer<Ignore, byte[]> consumer, Error error)
        {
            FileLogger.Log($"[Kafka] [{consumer.Name}] [{string.Concat(consumer.Subscription)}]: " + error.Reason);
        }
        void IDisposable.Dispose()
        {
            try
            {
                _consumer?.Close();
            }
            catch (Exception error)
            {
                FileLogger.Log($"[Kafka] ERROR {ExceptionHelper.GetErrorText(error)}");
            }

            try
            {
                _consumer?.Dispose();
            }
            catch (Exception error)
            {
                FileLogger.Log($"[Kafka] ERROR {ExceptionHelper.GetErrorText(error)}");
            }

            _result = null;
            _consumer = null;
        }

        private void InitializeMetadata()
        {
            if (!new MetadataService()
                .UseDatabaseProvider(_provider)
                .UseConnectionString(_connectionString)
                .TryOpenInfoBase(out InfoBase infoBase, out string error))
            {
                throw new Exception(error);
            }

            _yearOffset = infoBase.YearOffset;

            _queue = infoBase.GetApplicationObjectByName(_metadataName);

            if (_queue == null)
            {
                throw new Exception($"Объект метаданных \"{_metadataName}\" не найден.");
            }

            _version = GetDataContractVersion(in _queue);

            if (_version < 1)
            {
                throw new Exception(DATABASE_INTERFACE_IS_NOT_SUPPORTED_ERROR);
            }
        }
        private int GetDataContractVersion(in ApplicationObject queue)
        {
            DbInterfaceValidator validator = new DbInterfaceValidator();

            return validator.GetIncomingInterfaceVersion(in queue);
        }
        private DatabaseProvider GetDatabaseProviderFromConnectionString(in string connectionString)
        {
            return connectionString.StartsWith("Host")
                ? DatabaseProvider.PostgreSQL
                : DatabaseProvider.SQLServer;
        }
        
        private void ConfigureConsumer()
        {
            if (_consumer == null)
            {
                _consumer = new ConsumerBuilder<Ignore, byte[]>(_options)
                    .SetLogHandler(_logHandler)
                    .SetErrorHandler(_errorHandler)
                    .Build();

                _consumer.Subscribe(_topic);
            }
        }
        private IMessageProducer GetMessageProducer()
        {
            if (_provider == DatabaseProvider.SQLServer)
            {
                return new MsMessageProducer(in _connectionString, _queue);
            }
            else
            {
                return new PgMessageProducer(in _connectionString, _queue);
            }
        }
        private void GetHeaderValues(in Headers headers, out string sender, out string messageType)
        {
            sender = string.Empty;
            messageType = string.Empty;

            if (headers == null || headers.Count == 0)
            {
                return;
            }

            foreach (var header in headers)
            {
                if (header.Key == "sender")
                {
                    sender = Encoding.UTF8.GetString(header.GetValueBytes());
                }
                else if (header.Key == "message.type")
                {
                    messageType = Encoding.UTF8.GetString(header.GetValueBytes());
                }
            }
        }
        private IncomingMessageDataMapper ProduceMessage(in Message<Ignore, byte[]> message)
        {
            if (_version == 1)
            {
                return ProduceMessage1(in message);
            }
            else if (_version == 10)
            {
                return ProduceMessage10(in message);
            }
            else if (_version == 11)
            {
                return ProduceMessage11(in message);
            }
            else if (_version == 12)
            {
                return ProduceMessage12(in message);
            }
            else
            {
                return null;
            }
        }
        private IncomingMessageDataMapper ProduceMessage1(in Message<Ignore, byte[]> input)
        {
            GetHeaderValues(input.Headers, out string sender, out string messageType);

            V1.IncomingMessage message = IncomingMessageDataMapper.Create(_version) as V1.IncomingMessage;

            message.DateTimeStamp = DateTime.Now.AddYears(_yearOffset);
            message.MessageNumber = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
            message.ErrorCount = 0;
            message.ErrorDescription = string.Empty;
            message.Headers = string.Empty;
            message.Sender = sender;
            message.MessageType = messageType;
            message.MessageBody = (input.Value == null ? string.Empty : Encoding.UTF8.GetString(input.Value));

            return message;
        }
        private IncomingMessageDataMapper ProduceMessage10(in Message<Ignore, byte[]> input)
        {
            GetHeaderValues(input.Headers, out string sender, out string messageType);

            V10.IncomingMessage message = IncomingMessageDataMapper.Create(_version) as V10.IncomingMessage;

            message.Uuid = Guid.NewGuid();
            message.DateTimeStamp = DateTime.Now.AddYears(_yearOffset);
            message.MessageNumber = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
            message.ErrorCount = 0;
            message.ErrorDescription = string.Empty;
            message.Sender = sender;
            message.MessageType = messageType;
            message.MessageBody = (input.Value == null ? string.Empty : Encoding.UTF8.GetString(input.Value));

            return message;
        }
        private IncomingMessageDataMapper ProduceMessage11(in Message<Ignore, byte[]> input)
        {
            GetHeaderValues(input.Headers, out string sender, out string messageType);

            V11.IncomingMessage message = IncomingMessageDataMapper.Create(_version) as V11.IncomingMessage;

            message.Uuid = Guid.NewGuid();
            message.DateTimeStamp = DateTime.Now.AddYears(_yearOffset);
            message.MessageNumber = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
            message.ErrorCount = 0;
            message.ErrorDescription = string.Empty;
            message.Headers = string.Empty;
            message.Sender = sender;
            message.MessageType = messageType;
            message.MessageBody = (input.Value == null ? string.Empty : Encoding.UTF8.GetString(input.Value));

            return message;
        }
        private IncomingMessageDataMapper ProduceMessage12(in Message<Ignore, byte[]> input)
        {
            GetHeaderValues(input.Headers, out string sender, out string messageType);

            V12.IncomingMessage message = IncomingMessageDataMapper.Create(_version) as V12.IncomingMessage;

            message.Uuid = Guid.NewGuid();
            message.DateTimeStamp = DateTime.Now.AddYears(_yearOffset);
            message.MessageNumber = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
            message.ErrorCount = 0;
            message.ErrorDescription = string.Empty;
            message.Headers = string.Empty;
            message.Sender = sender;
            message.MessageType = messageType;
            message.MessageBody = (input.Value == null ? string.Empty : Encoding.UTF8.GetString(input.Value));

            return message;
        }
    }
}