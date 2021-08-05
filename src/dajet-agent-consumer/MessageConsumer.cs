using DaJet.Agent.MessageHandlers;
using DaJet.Metadata;
using DaJet.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DaJet.Agent.Consumer
{
    internal sealed class ConsumerInfo
    {
        internal string Exchange { get; set; }
        internal EventingBasicConsumer Consumer { get; set; }
    }
    public interface IMessageConsumer : IDisposable
    {
        void PushConsume();
        void PullConsume(CancellationToken stoppingToken);
    }
    public sealed class MessageConsumer : IMessageConsumer
    {
        private const string LOG_TOKEN = "C-MSG";
        private IConnection Connection { get; set; }
        private ConcurrentDictionary<string, string> ConsumerTags { get; set; } = new ConcurrentDictionary<string, string>();
        private ConcurrentDictionary<string, ConsumerInfo> Exchanges { get; set; } = new ConcurrentDictionary<string, ConsumerInfo>();
        private ConcurrentDictionary<string, Task> PullingConsumers { get; set; } = new ConcurrentDictionary<string, Task>();
        private IServiceProvider Services { get; set; }
        private MessageConsumerSettings Settings { get; set; }
        public MessageConsumer(IServiceProvider serviceProvider, IOptions<MessageConsumerSettings> options)
        {
            Settings = options.Value;
            Services = serviceProvider;
        }
        private IConnection CreateConnection()
        {
            IConnectionFactory factory = new ConnectionFactory()
            {
                HostName = Settings.MessageBrokerSettings.HostName,
                UserName = Settings.MessageBrokerSettings.UserName,
                Password = Settings.MessageBrokerSettings.Password,
                Port = Settings.MessageBrokerSettings.PortNumber,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };
            if (Settings.MessageBrokerSettings.FrameMaxSize > 0)
            {
                factory.RequestedFrameMax = Settings.MessageBrokerSettings.FrameMaxSize;
            }
            if (Settings.MessageBrokerSettings.ConsumeTimeOut > 0)
            {
                factory.ContinuationTimeout = TimeSpan.FromSeconds(Settings.MessageBrokerSettings.ConsumeTimeOut);
            }
            return factory.CreateConnection();
        }
        private void InitializeConnection()
        {
            if (Connection == null)
            {
                Connection = CreateConnection();
            }
            else if (!Connection.IsOpen)
            {
                Connection.Dispose();
                Connection = CreateConnection();
            }
        }
        public void Dispose()
        {
            if (Connection != null)
            {
                if (Connection.IsOpen)
                {
                    Connection.Close();
                }
                Connection.Dispose();
                Connection = null;
            }
        }
        private void DisposeChannel(IModel channel)
        {
            if (channel != null)
            {
                channel.Dispose();
            }
        }
        private void DisposeConsumer(EventingBasicConsumer consumer)
        {
            if (consumer == null) return;
            consumer.Received -= ProcessMessage;
        }
        private void DisposeConsumerTags(string exchange)
        {
            string[] tags = ConsumerTags
                .Where(kvp => kvp.Value == exchange)
                .Select(kvp => kvp.Key).ToArray();
            
            if (tags == null) return;

            for (int i = 0; i < tags.Length; i++)
            {
                _ = ConsumerTags.TryRemove(tags[i], out _);
            }
        }
                
        private string CreateExchangeName(string sender)
        {
            return $"РИБ.{sender}.{Settings.ThisNode}";
        }

        #region "Consumer: push API"

        public void PushConsume()
        {
            if (Settings.MessageBrokerSettings.ConsumeMode != 0) throw new InvalidOperationException("Eventing is not allowed in pull consumer mode!");

            if (Settings.DebugMode) FileLogger.Log(LOG_TOKEN, "Initializing connection to RabbitMQ ...");
            
            InitializeConnection();

            if (Settings.DebugMode)
            {
                if (Connection == null) FileLogger.Log(LOG_TOKEN, "Connection to RabbitMQ is NULL.");
                else
                {
                    string logMessage = "Connection to RabbitMQ: IsOpen = " + Connection.IsOpen.ToString();
                    if (!Connection.IsOpen && Connection.CloseReason != null)
                    {
                        logMessage += ", CloseReason = " + Connection.CloseReason.ReplyText;
                    }
                    FileLogger.Log(LOG_TOKEN, logMessage);
                }
            }
            if (Settings.DebugMode) FileLogger.Log(LOG_TOKEN, "Initializing channels for RabbitMQ ...");
            
            InitializeEventingConsumers();

            if (Settings.DebugMode)
            {
                if (Exchanges.Count == 0)
                {
                    FileLogger.Log(LOG_TOKEN, "None channel for RabbitMQ is initialized.");
                }
                else
                {
                    foreach (var consumer in Exchanges)
                    {
                        string logMessage = "Channel \"" + consumer.Key + "\": ";
                        if (consumer.Value == null || consumer.Value.Consumer == null)
                        {
                            logMessage += "consumer is NULL.";
                        }
                        else if (consumer.Value != null && consumer.Value.Consumer != null)
                        {
                            logMessage += "consumer " + (consumer.Value.Consumer.IsRunning ? "is running" : "is not running") + ".";
                            if (consumer.Value.Consumer.ConsumerTags != null)
                            {
                                if (consumer.Value.Consumer.ConsumerTags.Length > 0)
                                {
                                    logMessage += " Tags: ";
                                }
                                foreach (string tag in consumer.Value.Consumer.ConsumerTags)
                                {
                                    logMessage += tag + ", ";
                                }
                                if (consumer.Value.Consumer.ConsumerTags.Length > 0)
                                {
                                    logMessage = logMessage.TrimEnd(new char[] { ',', ' ' });
                                }
                                logMessage += ".";
                            }
                            if (consumer.Value.Consumer.ShutdownReason != null)
                            {
                                logMessage += " ShutdownReason: " + consumer.Value.Consumer.ShutdownReason.ReplyText;
                            }
                        }
                        FileLogger.Log(LOG_TOKEN, logMessage);
                    }
                }
            }
        }
        private void InitializeEventingConsumers()
        {
            if (Settings.DebugMode) FileLogger.Log(LOG_TOKEN, "Consuming messages from " + Settings.SenderNodes.Count.ToString() + " senders.");
            foreach (string sender in Settings.SenderNodes)
            {
                string exchange = CreateExchangeName(sender);
                if (Settings.DebugMode) FileLogger.Log(LOG_TOKEN, "Sender: " + sender + " (" + exchange + ").");
                if (Exchanges.TryGetValue(exchange, out ConsumerInfo consumerInfo))
                {
                    if (!IsConsumerHealthy(consumerInfo.Consumer))
                    {
                        ResetConsumer(consumerInfo, 0);
                    }
                }
                else
                {
                    StartConsumerTask(exchange);
                }
            }
        }
        private bool IsConsumerHealthy(EventingBasicConsumer consumer)
        {
            return (consumer != null
                && consumer.Model != null
                && consumer.Model.IsOpen
                && consumer.IsRunning);
        }
        private void StartConsumerTask(string exchange)
        {
            _ = Task.Factory.StartNew(StartNewConsumer, exchange,
                    CancellationToken.None,
                    TaskCreationOptions.LongRunning,
                    TaskScheduler.Default);
        }
        private void StartNewConsumer(object exchangeName)
        {
            if (!(exchangeName is string exchange)) return;

            IModel channel = null;
            EventingBasicConsumer consumer = null;

            try
            {
                channel = Connection.CreateModel();
                channel.BasicQos(0, 1, false);

                consumer = new EventingBasicConsumer(channel);
                consumer.Received += ProcessMessage;

                string tag = channel.BasicConsume(exchange, false, consumer);
                if (Settings.DebugMode)
                {
                    FileLogger.Log(LOG_TOKEN, "New consumer [" + tag + "] is started for exchange [" + exchange + "].");
                }
                _ = ConsumerTags.TryAdd(tag, exchange);
            }
            catch (Exception error)
            {
                DisposeChannel(channel);
                DisposeConsumer(consumer);
                FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
                throw; // Завершаем поток (задачу) с ошибкой
            }

            if (Exchanges.TryGetValue(exchange, out ConsumerInfo consumerInfo))
            {
                if (consumerInfo.Consumer != null)
                {
                    DisposeChannel(consumerInfo.Consumer.Model);
                    DisposeConsumer(consumerInfo.Consumer);
                }
                consumerInfo.Consumer = consumer;
            }
            else
            {
                consumerInfo = new ConsumerInfo()
                {
                    Exchange = exchange,
                    Consumer = consumer
                };
                _ = Exchanges.TryAdd(exchange, consumerInfo);
            }
        }
        private void ResetConsumer(string consumerTag)
        {
            if (!ConsumerTags.TryGetValue(consumerTag, out string exchange)) return;
            if (!Exchanges.TryGetValue(exchange, out ConsumerInfo consumerInfo)) return;
            if (consumerInfo == null) return;
            
            _ = ConsumerTags.TryRemove(consumerTag, out _);

            ResetConsumer(consumerInfo, Settings.CriticalErrorDelay * 1000);
        }
        private void ResetConsumer(ConsumerInfo consumerInfo, int timeout)
        {
            if (consumerInfo == null || consumerInfo.Consumer == null) return;

            DisposeChannel(consumerInfo.Consumer.Model);
            DisposeConsumer(consumerInfo.Consumer);
            DisposeConsumerTags(consumerInfo.Exchange);
                        
            if (timeout > 0)
            {
                Task.Delay(timeout).Wait();
            }
            
            StartConsumerTask(consumerInfo.Exchange);
        }
        private void UnsubscribeConsumer(EventingBasicConsumer consumer, string consumerTag)
        {
            try
            {
                if (IsConsumerHealthy(consumer))
                {
                    consumer.Model.BasicCancel(consumerTag);
                }
            }
            catch (Exception error)
            {
                FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
            }
            finally
            {
                DisposeChannel(consumer.Model);
                DisposeConsumer(consumer);
            }
            FileLogger.Log(LOG_TOKEN, $"Consumer tag \"{consumerTag}\" has been unsubscribed.");
        }
        private void RemovePoisonMessage(string exchange, EventingBasicConsumer consumer, ulong deliveryTag)
        {
            try
            {
                consumer.Model.BasicNack(deliveryTag, false, false);
                FileLogger.Log(LOG_TOKEN, "Poison message (bad format) has been removed from queue \"" + exchange + "\".");
            }
            catch (Exception error)
            {
                FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
                FileLogger.Log(LOG_TOKEN, "Failed to Nack message for exchange \"" + exchange + "\".");
            }
        }
        private void ProcessMessage(object sender, BasicDeliverEventArgs args)
        {
            if (!(sender is EventingBasicConsumer consumer)) return;

            if (!ConsumerTags.TryGetValue(args.ConsumerTag, out string exchange))
            {
                UnsubscribeConsumer(consumer, args.ConsumerTag);
                return;
            }

            JsonDataTransferMessage dataTransferMessage = GetJsonDataTransferMessage(args);
            if (dataTransferMessage == null)
            {
                RemovePoisonMessage(exchange, consumer, args.DeliveryTag);
                return;
            }

            //if (Settings.UseMessageHandlers)
            //{
            //    ProcessMessages(dataTransferMessage);
            //}

            bool success = true;
            IDatabaseMessageProducer producer = Services.GetService<IDatabaseMessageProducer>();
            try
            {
                DatabaseMessage message = producer.ProduceMessage(dataTransferMessage);
                if (Settings.DebugMode) FileLogger.Log(LOG_TOKEN, "Start insert message to database.");
                success = producer.InsertMessage(message);
                if (Settings.DebugMode) FileLogger.Log(LOG_TOKEN, "End insert message to database. Success = " + success.ToString());
                if (success)
                {
                    if (Settings.DebugMode) FileLogger.Log(LOG_TOKEN, "Send ACK to RabbitMQ. DeliveryTag = " + args.DeliveryTag.ToString());
                    consumer.Model.BasicAck(args.DeliveryTag, false);
                    if (Settings.DebugMode) FileLogger.Log(LOG_TOKEN, "ACK to RabbitMQ has been sent successfully.");
                }
            }
            catch (Exception error)
            {
                success = false;
                FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
            }

            if (!success)
            {
                ResetConsumer(args.ConsumerTag); // return unacked messages back to queue in the same order (!)
                FileLogger.Log(LOG_TOKEN, "Failed to process message. Consumer (tag = " + args.ConsumerTag.ToString() + ") for exchange \"" + exchange + "\" has been reset.");
            }
        }

        private JsonDataTransferMessage GetJsonDataTransferMessage(BasicDeliverEventArgs args)
        {
            byte[] body = args.Body.ToArray();
            string messageBody = Encoding.UTF8.GetString(body);

            JsonDataTransferMessage dataTransferMessage = null;

            if (string.IsNullOrWhiteSpace(args.BasicProperties.Type))
            {
                try
                {
                    dataTransferMessage = JsonSerializer.Deserialize<JsonDataTransferMessage>(messageBody);
                }
                catch (Exception error)
                {
                    FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
                }
            }
            else
            {
                dataTransferMessage = new JsonDataTransferMessage()
                {
                    Sender = (args.BasicProperties.AppId == null ? string.Empty : args.BasicProperties.AppId)
                };
                dataTransferMessage.Objects.Add(new JsonDataTransferObject()
                {
                    Type = (args.BasicProperties.Type == null ? string.Empty : args.BasicProperties.Type),
                    Body = messageBody,
                    Operation = string.Empty
                });

                if (args.BasicProperties.Headers != null)
                {
                    if (args.BasicProperties.Headers.TryGetValue("OperationType", out object value))
                    {
                        if (value is byte[] operationType)
                        {
                            dataTransferMessage.Objects[0].Operation = Encoding.UTF8.GetString(operationType);
                        }
                    }
                }

                //if (args.BasicProperties.Headers != null)
                //{
                //    var header = args.BasicProperties.Headers.Where(hdr => hdr.Key == "CC").FirstOrDefault();
                //    if (header.Value is List<object> copies)
                //    {
                //        message.AppendLine($"CC routing keys:");
                //        for (int i = 0; i < copies.Count; i++)
                //        {
                //            if (!(copies[i] is byte[] copy))
                //            {
                //                continue;
                //            }
                //            string routingKey = Encoding.UTF8.GetString(copy);
                //            message.AppendLine($"{i}. {routingKey}");
                //        }
                //    }
                //}
            }

            return dataTransferMessage;
        }

        #endregion

        #region "Consumer: pull API"

        private CancellationToken _stoppingToken;
        public void PullConsume(CancellationToken stoppingToken)
        {
            if (Settings.MessageBrokerSettings.ConsumeMode == 0) throw new InvalidOperationException("Pulling is not allowed in eventing consumer mode!");

            if (_stoppingToken == null)
            {
                _stoppingToken = stoppingToken;
            }

            InitializeConnection();
            InitializePullingConsumers(stoppingToken);
        }
        private void InitializePullingConsumers(CancellationToken stoppingToken)
        {
            foreach (string sender in Settings.SenderNodes)
            {
                string exchange = CreateExchangeName(sender);

                if (PullingConsumers.TryGetValue(exchange, out Task consumerTask))
                {
                    if (consumerTask.IsCompleted)
                    {
                        PullingConsumers[exchange] = StartPullConsumerTask(exchange);
                    }
                }
                else
                {
                    _ = PullingConsumers.TryAdd(exchange, StartPullConsumerTask(exchange));
                }
            }
        }
        private Task StartPullConsumerTask(string exchange)
        {
            return Task.Factory.StartNew(RunPullingConsumer, exchange,
                _stoppingToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }
        private async Task RunPullingConsumer(object exchangeName)
        {
            if (!(exchangeName is string exchange)) return;

            IModel channel = Connection.CreateModel();
            
            IDatabaseMessageProducer producer = Services.GetService<IDatabaseMessageProducer>();

            while (!_stoppingToken.IsCancellationRequested)
            {
                BasicGetResult result = GetMessage(channel, exchange);
                while (result != null)
                {
                    JsonDataTransferMessage dataTransferMessage = GetJsonData(result);
                    if (dataTransferMessage == null) // bad format message
                    {
                        AckMessage(channel, result); // ack anyway and continue receive messages
                        break;
                    }
                    
                    //if (Settings.UseMessageHandlers)
                    //{
                    //    ProcessMessages(dataTransferMessage);
                    //}

                    DatabaseMessage message = producer.ProduceMessage(dataTransferMessage);

                    bool success = producer.InsertMessage(message);

                    if (!success) // database critical error
                    {
                        channel.Dispose();
                        return;
                    }

                    AckMessage(channel, result);

                    result = GetMessage(channel, exchange);
                }
                await Task.Delay(TimeSpan.FromMinutes(1), _stoppingToken);
            }
            channel.Dispose();
        }
        private JsonDataTransferMessage GetJsonData(BasicGetResult result)
        {
            JsonDataTransferMessage dataTransferMessage = null;
            try
            {
                byte[] body = result.Body.ToArray();
                string messageBody = Encoding.UTF8.GetString(body);
                dataTransferMessage = JsonSerializer.Deserialize<JsonDataTransferMessage>(messageBody);
            }
            catch (Exception error)
            {
                FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
            }
            return dataTransferMessage;
        }
        private BasicGetResult GetMessage(IModel channel, string queueName)
        {
            BasicGetResult result = null;
            try
            {
                result = channel.BasicGet(queueName, false);
            }
            catch (Exception error)
            {
                string errorMessage = "Error receiving message for queue [" + queueName + "]: "
                    + ExceptionHelper.GetErrorText(error);
                FileLogger.Log(LOG_TOKEN, errorMessage);

                channel.Dispose();

                throw error;
            }
            return result;
        }
        private void AckMessage(IModel channel, BasicGetResult result)
        {
            try
            {
                channel.BasicAck(result.DeliveryTag, false);
            }
            catch (Exception error)
            {
                string errorMessage = "Error confirming message received for queue [" + result.Exchange + "]: "
                    + ExceptionHelper.GetErrorText(error);
                FileLogger.Log(LOG_TOKEN, errorMessage);

                channel.Dispose();

                throw error;
            }
        }

        #endregion

        private void ProcessMessages(JsonDataTransferMessage envelope)
        {
            if (envelope == null) return;

            IMessageHandler messageHandler = new ШтрихкодыУпаковокЗаказовКлиентовMessageHandler();
            foreach (JsonDataTransferObject message in envelope.Objects)
            {
                try
                {
                    messageHandler.ProcessMessage("input", message.Type, message.Body);
                }
                catch (Exception error)
                {
                    FileLogger.Log(ExceptionHelper.GetErrorText(error));
                }
            }
        }
    }
}