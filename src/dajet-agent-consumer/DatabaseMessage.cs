using System;

namespace DaJet.Agent.Consumer
{
    public sealed class DatabaseMessage
    {
        public Guid Uuid { get; set; }
        public long Code { get; set; }
        public byte[] Version { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public string Sender { get; set; }
        public string OperationType { get; set; }
        public string MessageType { get; set; }
        public string MessageBody { get; set; }
    }
}