using DaJet.Metadata;
using DaJet.RabbitMQ;

namespace DaJet.Agent.Service
{
    internal class DaJetAgentOptions
    {
        internal int RefreshTimeout { get; set; } = 10; // seconds
        internal string ConnectionString { get; set; } = string.Empty;
        internal DatabaseProvider DatabaseProvider { get; set; } = DatabaseProvider.SQLServer;
        internal string HostName { get; set; } = "localhost";
        internal string VirtualHost { get; set; } = "/";
        internal int PortNumber { get; set; } = 5672;
        internal string UserName { get; set; } = "guest";
        internal string Password { get; set; } = "guest";
        internal string AggregatorTopic { get; set; } = "РИБ.АПО";
        internal string DispatcherTopic { get; set; } = "РИБ.ERP";
        internal ExchangeRoles ExchangeRole { get; set; } = ExchangeRoles.None;
    }
}