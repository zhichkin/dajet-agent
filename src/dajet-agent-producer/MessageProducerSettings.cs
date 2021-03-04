namespace DaJet.Agent.Producer
{
    public sealed class MessageProducerSettings
    {
        public int CriticalErrorDelay { get; set; } = 300; // seconds
        public DatabaseSettings DatabaseSettings { get; set; }
        public MessageBrokerSettings MessageBrokerSettings { get; set; }
    }
    public sealed class DatabaseSettings
    {
        public string ConnectionString { get; set; }
        public int MessagesPerTransaction { get; set; } = 1000;
        public int DatabaseQueryingPeriodicity { get; set; } = 60; // seconds
        public int WaitForNotificationTimeout { get; set; } = 180; // seconds
        public DatabaseQueue DatabaseQueue { get; set; }
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