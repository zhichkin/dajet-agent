namespace DaJet.Agent.MessageHandlers
{
    public interface IMessageHandler
    {
        void ProcessMessage(string direction, string messageType, string messageBody);
    }
}