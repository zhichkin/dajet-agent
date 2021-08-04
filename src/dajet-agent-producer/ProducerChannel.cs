using RabbitMQ.Client;
using System.Collections.Generic;

namespace DaJet.Agent.Producer
{
    public sealed class ProducerChannel
    {
        public IModel Channel { get; set; }
        public IBasicProperties Properties { get; set; }
        public List<Queue<DatabaseMessage>> Queues { get; } = new List<Queue<DatabaseMessage>>();
        public bool IsHealthy
        {
            get
            {
                return (Channel != null && Channel.IsOpen);
            }
        }
    }
}