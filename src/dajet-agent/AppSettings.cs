using System.Collections.Generic;

namespace DaJet.Agent.Service
{
    public sealed class AppSettings
    {
        public int LogSize { get; set; } = 1024 * 1024; // kilobytes
        public bool UseProducer { get; set; } = true;
        public bool UseConsumer { get; set; } = true;
        public bool UseDeliveryTraking { get; set; } = false;
        public bool UseTelegram { get; set; } = false;
        public string BotToken { get; set; } = string.Empty;
        public string BotChannel { get; set; } = string.Empty;
        public string AppCatalog { get; set; } = string.Empty;
        public int RefreshTimeout { get; set; } = 600; // seconds
        public List<string> ExchangePlans { get; set; } = new List<string>();
    }
}