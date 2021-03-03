using System;

namespace DaJet.Agent.Consumer
{
    public sealed class DatabaseMessage
    {
        public long Code { get; set; }
        public long Version { get; set; }
        public string MessageType { get; set; }
        public string MessageBody { get; set; }
        public string OperationType { get; set; }
        public DateTime OperationDate { get; set; }
    }
}