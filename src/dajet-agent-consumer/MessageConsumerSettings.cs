namespace DaJet.Agent.Consumer
{
    public sealed class MessageConsumerSettings
    {
        public int CriticalErrorDelay { get; set; } = 300; // seconds
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}