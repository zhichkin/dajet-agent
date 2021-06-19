using DaJet.Metadata;

namespace DaJet.Agent.Producer
{
    public sealed class MessageProducerSettings
    {
        public int CriticalErrorDelay { get; set; } = 300; // seconds
        public bool UseMessageHandlers { get; set; } = false;
        public DatabaseSettings DatabaseSettings { get; set; } = new DatabaseSettings();
        public MessageBrokerSettings MessageBrokerSettings { get; set; } = new MessageBrokerSettings();
    }
    public sealed class DatabaseSettings
    {
        public DatabaseProviders DatabaseProvider { get; set; } = DatabaseProviders.SQLServer;
        public string ConnectionString { get; set; } = string.Empty;
        public int MessagesPerTransaction { get; set; } = 1000;
        public int DatabaseQueryingPeriodicity { get; set; } = 60; // seconds
        public bool UseNotifications { get; set; } = false;
        public string NotificationQueueName { get; set; } = "dajet-agent-export-queue";
        public int WaitForNotificationTimeout { get; set; } = 180; // seconds
        public DatabaseQueue DatabaseQueue { get; set; } = new DatabaseQueue();
    }
    public sealed class MessageBrokerSettings
    {
        public string HostName { get; set; } = "localhost";
        public int PortNumber { get; set; } = 5672;
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public int ConfirmationTimeout { get; set; } = 1; // seconds
    }
}