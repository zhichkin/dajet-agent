namespace DaJet.Agent.Service
{
    public sealed class AppSettings
    {
        public int LogSize { get; set; } = 64 * 1024; // 64 Kb = 65536 bytes
    }
}