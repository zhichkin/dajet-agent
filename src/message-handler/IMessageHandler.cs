namespace DaJet.Agent.MessageHandlers
{
    public interface IMessageHandler
    {
        void ProcessMessage(string messageType, string messageBody);
    }
}