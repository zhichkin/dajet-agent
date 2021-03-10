using DaJet.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DaJet.Agent.Consumer
{
    public interface IMessageConsumer : IDisposable
    {
        void Consume();
    }
    public sealed class MessageConsumer : IMessageConsumer
    {
        private IModel Channel { get; set; }
        private IConnection Connection { get; set; }
        private List<string> ConsumerTags { get; set; } = new List<string>();
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
        private void InitializeChannel()
        {
            InitializeConnection();

            if (Channel == null)
            {
                Channel = Connection.CreateModel();
                Channel.BasicQos(0, Settings.MessageBrokerSettings.ConsumerPrefetchCount, false);
            }
            else if (Channel.IsClosed)
            {
                Channel.Dispose();
                Channel = Connection.CreateModel();
                Channel.BasicQos(0, Settings.MessageBrokerSettings.ConsumerPrefetchCount, false);
            }
        }
        public void Dispose()
        {
            if (Channel != null)
            {
                if (!Channel.IsClosed)
                {
                    foreach (string tag in ConsumerTags)
                    {
                        try
                        {
                            Channel.BasicCancel(tag);
                        }
                        catch (Exception error)
                        {
                            FileLogger.Log(ExceptionHelper.GetErrorText(error));
                        }
                    }
                    Channel.Close();
                }
                Channel.Dispose();
                Channel = null;
            }

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

        private string CreateExchangeName(string sender)
        {
            return $"РИБ.{sender}.{Settings.ThisNode}";
        }
        public void Consume()
        {
            InitializeChannel();

            foreach (string sender in Settings.SenderNodes)
            {
                string name = CreateExchangeName(sender);
                EventingBasicConsumer consumer = new EventingBasicConsumer(Channel);
                consumer.Received += ProcessMessage;
                try
                {
                    string tag = Channel.BasicConsume(name, false, consumer);
                    ConsumerTags.Add(tag);
                }
                catch (OperationInterruptedException rabbitError)
                {
                    if (!string.IsNullOrWhiteSpace(rabbitError.Message)
                        && rabbitError.Message.Contains("NOT_FOUND")
                        && rabbitError.Message.Contains(name))
                    {
                        // queue not found
                        FileLogger.Log(ExceptionHelper.GetErrorText(rabbitError));
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
        private void ProcessMessage(object sender, BasicDeliverEventArgs args)
        {
            if (!(sender is EventingBasicConsumer consumer)) return;

            bool success = true;

            JsonDataTransferMessage dataTransferMessage = null;
            try
            {
                byte[] body = args.Body.ToArray();
                string messageBody = Encoding.UTF8.GetString(body);
                dataTransferMessage = JsonSerializer.Deserialize<JsonDataTransferMessage>(messageBody);
            }
            catch (Exception error)
            {
                success = false;
                FileLogger.Log(ExceptionHelper.GetErrorText(error));
            }
            if (!success)
            {
                try
                {
                    // Remove poison message from queue
                    consumer.Model.BasicNack(args.DeliveryTag, false, false);
                    FileLogger.Log("Poison message (bad format) has been removed from queue \"" + args.Exchange + "\".");
                }
                catch (Exception error)
                {
                    FileLogger.Log(ExceptionHelper.GetErrorText(error));
                    FileLogger.Log("Failed to Nack message for exchange \"" + args.Exchange + "\".");
                }
                return;
            }

            IDatabaseMessageProducer producer = Services.GetService<IDatabaseMessageProducer>();
            DatabaseMessage message = producer.ProduceMessage(dataTransferMessage);
            try
            {
                success = producer.InsertMessage(message);
                if (success)
                {
                    consumer.Model.BasicAck(args.DeliveryTag, false);
                }
            }
            catch (Exception error)
            {
                success = false;
                FileLogger.Log(ExceptionHelper.GetErrorText(error));
            }

            if (!success)
            {
                FileLogger.Log("Failed to process message. Consumer for exchange \"" + args.Exchange + "\" will be reset.");
                // TODO: reset consumer
                //consumer.Model.BasicCancel(args.ConsumerTag);
                //ConsumerTags.Remove(args.ConsumerTag);
                //Task.Delay(5000).Wait();
            }
        }
    }
}