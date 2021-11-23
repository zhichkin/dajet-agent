using RabbitMQ.Client;
using System.Collections.Generic;

namespace DaJet.Agent.Producer
{
    public sealed class ProducerChannel
    {
        private ulong _currentDeliveryTag = 0UL;
        private readonly object ConfirmSyncRoot = new object();

        public IModel Channel { get; set; }
        public IBasicProperties Properties { get; set; }
        public ulong DeliveryTagToWait { get; set; } = 0UL;
        public ulong CurrentDeliveryTag
        {
            get { return _currentDeliveryTag; }
            set
            {
                lock (ConfirmSyncRoot)
                {
                    _currentDeliveryTag = value;
                }
            }
        }
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