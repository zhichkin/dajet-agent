using System;

namespace DaJet.Agent.Consumer
{
    public sealed class DatabaseMessage
    {
        public long Code { get; set; } = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        public Guid Uuid { get; set; } = Guid.NewGuid();
        public bool DeletionMark { get; set; } = false;
        public Guid PredefinedID { get; set; } = Guid.Empty;
        public DateTime DateTimeStamp { get; set; } = DateTime.Now.AddYears(2000);
        public string Sender { get; set; }
        public string OperationType { get; set; }
        public string MessageType { get; set; }
        public string MessageBody { get; set; }
    }
}