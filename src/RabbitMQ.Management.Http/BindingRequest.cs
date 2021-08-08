using System.Text.Json.Serialization;

namespace RabbitMQ.Management.Http
{
    internal sealed class BindingRequest
    {
        [JsonPropertyName("routing_key")] public string RoutingKey { get; set; }
    }
}