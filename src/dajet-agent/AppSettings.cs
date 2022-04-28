using System.Collections.Generic;

namespace DaJet.Agent.Service
{
    public sealed class AppSettings
    {
        public int LogSize { get; set; } = 1024 * 1024; // kilobytes
        public bool UseProducer { get; set; } = true;
        public bool UseConsumer { get; set; } = true;
        public string AppCatalog { get; set; } = string.Empty;
        public List<string> ExchangePlans { get; set; } = new List<string>();
    }
}