using DaJet.Metadata;
using System.Collections.Generic;

namespace DaJet.Agent.Consumer
{
    public sealed class MessageConsumerSettings
    {
        public int CriticalErrorDelay { get; set; } = 180; // seconds
        public DatabaseSettings DatabaseSettings { get; set; } = new DatabaseSettings();
        public MessageBrokerSettings MessageBrokerSettings { get; set; } = new MessageBrokerSettings();
        public string ThisNode { get; set; } = string.Empty;
        public List<string> SenderNodes { get; set; } = new List<string>();
    }
    public sealed class DatabaseSettings
    {
        public DatabaseProvider DatabaseProvider { get; set; } = DatabaseProvider.PostgreSQL;
        public string ConnectionString { get; set; } = string.Empty;
    }
    public sealed class MessageBrokerSettings
    {
        public string HostName { get; set; } = "localhost";
        public string VirtualHost { get; set; } = "/";
        public int PortNumber { get; set; } = 5672;
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
    }
}