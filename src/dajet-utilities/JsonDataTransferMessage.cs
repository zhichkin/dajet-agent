using System.Collections.Generic;

namespace DaJet.Utilities
{
    public sealed class JsonDataTransferMessage
    {
        public string Sender { get; set; }
        public List<JsonDataTransferObject> Objects { get; set; } = new List<JsonDataTransferObject>();
    }
    public sealed class JsonDataTransferObject
    {
        public string Type { get; set; }
        public string Body { get; set; }
        public string Operation { get; set; }
    }
}