using RabbitMQ.Client;

namespace DaJet.Agent.Producer
{
    public sealed class SendingChannel
    {
        public IModel Channel { get; set; }
        public IBasicProperties Properties { get; set; }
        public bool IsHealthy
        {
            get
            {
                return (Channel != null && Channel.IsOpen);
            }
        }
    }
}