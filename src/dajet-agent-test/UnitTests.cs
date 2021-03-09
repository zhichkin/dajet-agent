using DaJet.Agent.Producer;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaJet.Agent.Test
{
    [TestClass]
    public class UnitTests
    {
        private IOptions<MessageProducerSettings> Settings { get; set; }
        public UnitTests()
        {
            InitializeTestSettings();
        }
        private void InitializeTestSettings()
        {
            MessageProducerSettings settings = new MessageProducerSettings()
            {
                MessageBrokerSettings = new MessageBrokerSettings()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest",
                    PortNumber = 5672
                }
            };
            Settings = Options.Create(settings);
        }

        [TestMethod] public void CreateExchangeAndQueue()
        {
            string prefix = "–»¡";
            string mainNode = "MAIN";
            string[] rayNodes = new string[] { "N001", "N002" };
            IMessageProducer producer = new MessageProducer(Settings);

            for (int i = 0; i < rayNodes.Length; i++)
            {
                producer.CreateQueue($"{prefix}.{mainNode}.{rayNodes[i]}");
                producer.CreateQueue($"{prefix}.{rayNodes[i]}.{mainNode}");
            }
        }
    }
}