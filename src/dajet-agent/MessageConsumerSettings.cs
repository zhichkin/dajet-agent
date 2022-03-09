using DaJet.Metadata;
using System.Text;
using System.Web;

namespace DaJet.Agent.Consumer
{
    public sealed class MessageConsumerSettings
    {
        public int CriticalErrorDelay { get; set; } = 180; // seconds
        public DatabaseSettings DatabaseSettings { get; set; } = new DatabaseSettings();
        public MessageBrokerSettings MessageBrokerSettings { get; set; } = new MessageBrokerSettings();
    }
    public sealed class DatabaseSettings
    {
        public DatabaseProvider DatabaseProvider { get; set; } = DatabaseProvider.PostgreSQL;
        public string ConnectionString { get; set; } = string.Empty;
    }
    public sealed class MessageBrokerSettings
    {
        public string HostName { get; set; } = "localhost";
        public string VirtualHost { get; set; } = "/";
        public int PortNumber { get; set; } = 5672;
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string BuildUri()
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