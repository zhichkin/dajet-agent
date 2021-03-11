using DaJet.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        void Consume();
    }
    public sealed class MessageConsumer : IMessageConsumer
    {
        private const string LOG_TOKEN = "C-MSG";
        private IConnection Connection { get; set; }
        private ConcurrentDictionary<string, string> ConsumerTags { get; set; } = new ConcurrentDictionary<string, string>();
        private ConcurrentDictionary<string, ConsumerInfo> Exchanges { get; set; } = new ConcurrentDictionary<string, ConsumerInfo>();
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
        public void Consume()
        {
            InitializeConnection();
            InitializeConsumers();
        }
        private void InitializeConsumers()
        {
            foreach (string sender in Settings.SenderNodes)
            {
                string exchange = CreateExchangeName(sender);
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
                    TaskCreationOptions.DenyChildAttach,
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

            JsonDataTransferMessage dataTransferMessage = null;
            try
            {
                byte[] body = args.Body.ToArray();
                string messageBody = Encoding.UTF8.GetString(body);
                dataTransferMessage = JsonSerializer.Deserialize<JsonDataTransferMessage>(messageBody);
            }
            catch (Exception error)
            {
                FileLogger.Log(LOG_TOKEN, ExceptionHelper.GetErrorText(error));
            }

            if (dataTransferMessage == null)
            {
                RemovePoisonMessage(exchange, consumer, args.DeliveryTag);
                return;
            }

            bool success = true;
            IDatabaseMessageProducer producer = Services.GetService<IDatabaseMessageProducer>();
            try
            {
                DatabaseMessage message = producer.ProduceMessage(dataTransferMessage);
                success = producer.InsertMessage(message);
                if (success)
                {
                    consumer.Model.BasicAck(args.DeliveryTag, false);
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
                FileLogger.Log(LOG_TOKEN, "Failed to process message. Consumer for exchange \"" + exchange + "\" has been reset.");
            }
        }
    }
}