using RabbitMQ.Client;

namespace DaJet.Benchmarks
{
    public sealed class RabbitMessageProducer
    {
        private const string HostName = "localhost";
        private const string Exchange = "РИБ.N001.MAIN";
        private const string UserName = "guest";
        private const string Password = "guest";
        private const int PortNumber = 5672;
        private const string TestMessage = "{ \"test\": \"test\" }";

        private IConnection Connection;
        private IModel Channel;
        private IBasicProperties Properties;

        public RabbitMessageProducer()
        {

        }
    }
}