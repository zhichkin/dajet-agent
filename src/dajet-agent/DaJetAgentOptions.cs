using DaJet.Metadata;
using DaJet.RabbitMQ;
using System.Text;
using System.Web;

namespace DaJet.Agent.Service
{
    internal class DaJetAgentOptions
    {
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
        internal string ThisNode { get; set; } = string.Empty;
        internal bool UseDeliveryTracking { get; set; } = false;
        internal string GetRabbitMQUri()
        {
            string template = "amqp://{0}:{1}@{2}:{3}/{4}";

            return string.Format(
                template,
                HttpUtility.UrlEncode(UserName, Encoding.UTF8),
                HttpUtility.UrlEncode(Password, Encoding.UTF8),
                HttpUtility.UrlEncode(HostName, Encoding.UTF8),
                PortNumber,
                HttpUtility.UrlEncode(VirtualHost, Encoding.UTF8));
        }
    }
}