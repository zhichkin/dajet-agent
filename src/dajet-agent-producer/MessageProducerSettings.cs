using System.Collections.Generic;

namespace DaJet.Agent.Producer
{
    public sealed class MessageProducerSettings
    {
        public int LogSize { get; set; } = 64 * 1024; // 64 Kb = 65536 bytes
        public string ConnectionString { get; set; }
        public string HostName { get; set; } = "localhost";
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public int PortNumber { get; set; } = 5672;
        public string QueueName { get; set; } = "dajet-queue";
        public string RoutingKey { get; set; } = string.Empty;
        public string ExchangeName { get; set; } = "dajet-exchange";
        public int ConfirmationTimeout { get; set; } = 3; // seconds
        public Dictionary<string, string> MessageTypeRouting { get; set; } = new Dictionary<string, string>();
        public int ReceivingMessagesPeriodicity { get; set; } = 60000; // 1 minute
        public int WaitForNotificationTimeout { get; set; } = 180000; // 3 minutes
        public int CriticalErrorDelay { get; set; } = 300000; // 5 minutes
        public int MessagesPerTransaction { get; set; } = 1000;
    }
}