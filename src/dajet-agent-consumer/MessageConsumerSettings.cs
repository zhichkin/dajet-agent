using DaJet.Metadata;
using System.Collections.Generic;

namespace DaJet.Agent.Consumer
{
    public sealed class MessageConsumerSettings
    {
        public bool DebugMode { get; set; } = false;
        public int CriticalErrorDelay { get; set; } = 300; // seconds
        public bool UseMessageHandlers { get; set; } = false;
        public DatabaseSettings DatabaseSettings { get; set; } = new DatabaseSettings();
        public MessageBrokerSettings MessageBrokerSettings { get; set; } = new MessageBrokerSettings();
        public string ThisNode { get; set; } = string.Empty;
        public List<string> SenderNodes { get; set; } = new List<string>();
    }
    public sealed class DatabaseSettings
    {
        public DatabaseProvider DatabaseProvider { get; set; } = DatabaseProvider.SQLServer;
        public string ConnectionString { get; set; } = string.Empty;
        public DatabaseQueue DatabaseQueue { get; set; } = new DatabaseQueue();
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
        public int ConsumeMode { get; set; } = 0; // 0 - eventing consumer (push api), 1 - basic get consumer (pull api)
        public uint FrameMaxSize { get; set; } = 0;
        public int ConsumeTimeOut { get; set; } = 0; // applies only for pull api
    }
}