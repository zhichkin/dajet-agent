namespace DaJet.Agent.Service
{
    public sealed class AppSettings
    {
        public int LogSize { get; set; } = 64 * 1024; // 64 Kb = 65536 bytes
        public bool UseProducer { get; set; } = true;
        public bool UseConsumer { get; set; } = true;
        public bool UseWebServer { get; set; } = false;
        public string AppCatalog { get; set; } = string.Empty;
    }
}