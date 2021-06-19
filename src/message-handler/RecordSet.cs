using System.Collections.Generic;

namespace DaJet.Agent.MessageHandlers
{
    public sealed class RecordSet
    {
        public string TypeName { get; set; }
        public List<object> Records { get; set; } = new List<object>();
        public Dictionary<string, object> Filter { get; set; } = new Dictionary<string, object>();
    }
}