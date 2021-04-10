using System;

namespace DaJet.Agent.Consumer
{
    public sealed class DatabaseMessage
    {
        public long Code { get; set; } = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        public Guid Uuid { get; set; } = Guid.NewGuid();
        public DateTime DateTimeStamp { get; set; } = DateTime.Now.AddYears(2000);
        public string Sender { get; set; } = string.Empty;
        public string OperationType { get; set; } = string.Empty;
        public string MessageType { get; set; } = string.Empty;
        public string MessageBody { get; set; } = string.Empty;
        public int ErrorCount { get; set; } = 0;
        public string ErrorDescription { get; set; } = string.Empty;
    }
}