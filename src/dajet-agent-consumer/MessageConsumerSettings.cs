using System.Collections.Generic;

namespace DaJet.Agent.Consumer
{
    public sealed class MessageConsumerSettings
    {
        public int CriticalErrorDelay { get; set; } = 300; // seconds
        public DatabaseSettings DatabaseSettings { get; set; }
        public MessageBrokerSettings MessageBrokerSettings { get; set; }
        public string ThisNode { get; set; }
        public List<string> SenderNodes { get; set; } = new List<string>();
    }
    public sealed class DatabaseSettings
    {
        public string ConnectionString { get; set; }
        public DatabaseQueue DatabaseQueue { get; set; }
    }
    public sealed class MessageBrokerSettings
    {
        public string HostName { get; set; } = "localhost";
        public int PortNumber { get; set; } = 5672;
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        /// <summary>
        /// The value defines the max number of unacknowledged deliveries that are permitted on a consumer.
        /// </summary>
        public ushort ConsumerPrefetchCount { get; set; } = 1;
    }
}