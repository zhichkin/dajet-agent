namespace DaJet.Agent.Consumer
{
    public sealed class MessageConsumerSettings
    {
        public int LogSize { get; set; } = 64 * 1024; // 64 Kb = 65536 bytes
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int CriticalErrorDelay { get; set; } = 300000; // 5 minutes
    }
}