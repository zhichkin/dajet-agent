using System;
using System.Collections.Generic;

namespace DaJet.Agent.Producer
{
    public sealed class DatabaseMessage
    {
        public Guid Uuid { get; set; }
        public long Code { get; set; }
        public long Version { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public string Sender { get; set; }
        public string Recipients { get; set; }
        public string OperationType { get; set; }
        public string MessageType { get; set; }
        public string MessageBody { get; set; }
        public byte[] MessageBytes { get; set; }
    }
}