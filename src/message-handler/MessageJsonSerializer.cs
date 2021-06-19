using DaJet.Json;
using DaJet.Json.Converters;
using System.Text.Json;

namespace DaJet.Agent.MessageHandlers
{
    public interface IMessageJsonSerializer
    {
        ISerializationBinder Binder { get; }
        object FromJson(string json);
        string ToJson(object message);
    }
    public sealed class MessageJsonSerializer : IMessageJsonSerializer
    {
        private readonly IReferenceResolver _resolver = new JsonReferenceResolver();
        private readonly ISerializationBinder _binder = new JsonSerializationBinder();
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions();
        public MessageJsonSerializer()
        {
            _options.WriteIndented = true;
            _options.Converters.Add(new MessageJsonConverter(_binder, _resolver));
        }
        public ISerializationBinder Binder { get { return _binder; } }

        public object FromJson(string json)
        {
            return JsonSerializer.Deserialize<object>(json, _options);
        }

        public string ToJson(object message)
        {
            _resolver.Clear();
            return JsonSerializer.Serialize(message, _options);
        }
    }
}