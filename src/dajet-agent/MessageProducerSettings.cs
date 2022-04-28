using DaJet.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Web;

namespace DaJet.Agent.Producer
{
    public sealed class MessageProducerSettings
    {
        public int RefreshTimeout { get; set; } = 300; // seconds
        public int CriticalErrorDelay { get; set; } = 180; // seconds
        public DatabaseSettings DatabaseSettings { get; set; } = new DatabaseSettings();
        public MessageBrokerSettings MessageBrokerSettings { get; set; } = new MessageBrokerSettings();
    }
    public sealed class DatabaseSettings
    {
        public DatabaseProvider DatabaseProvider { get; set; } = DatabaseProvider.PostgreSQL;
        public string ConnectionString { get; set; } = string.Empty;
        public int MessagesPerTransaction { get; set; } = 1000;
        public int DatabaseQueryingPeriodicity { get; set; } = 60; // seconds
        [JsonIgnore] public bool UseNotifications { get; set; } = false;
        [JsonIgnore] public string NotificationQueueName { get; set; } = "dajet-agent-export-queue";
        [JsonIgnore] public int WaitForNotificationTimeout { get; set; } = 180; // seconds
    }
    public sealed class MessageBrokerSettings
    {
        public string HostName { get; set; } = "localhost";
        public string VirtualHost { get; set; } = "/";
        public int PortNumber { get; set; } = 5672;
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        [JsonIgnore] public int ConfirmationTimeout { get; set; } = 600; // seconds
        public string TopicExchange { get; set; } = string.Empty; // topic (durable)
        public int ExchangeRole { get; set; } = 0; // 0 - aggregator (Sender); 1 - dispatcher (Recipients)
        [JsonIgnore] public int CopyType { get; set; } = 0; // 0 - CarbonCopy; 1 - BlindCarbonCopy
        public string BuildUri()
        {
            string template = "amqp://{0}:{1}@{2}:{3}/{4}/{5}";

            return string.Format(
                template,
                HttpUtility.UrlEncode(UserName, Encoding.UTF8),
                HttpUtility.UrlEncode(Password, Encoding.UTF8),
                HttpUtility.UrlEncode(HostName, Encoding.UTF8),
                PortNumber,
                HttpUtility.UrlEncode(VirtualHost, Encoding.UTF8),
                HttpUtility.UrlEncode(TopicExchange, Encoding.UTF8));
        }
    }
}