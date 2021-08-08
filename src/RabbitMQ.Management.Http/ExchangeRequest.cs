﻿using System.Text.Json.Serialization;

namespace RabbitMQ.Management.Http
{
    internal sealed class ExchangeRequest
    {
        [JsonPropertyName("type")] public string Type { get; set; }
        [JsonPropertyName("durable")] public bool Durable { get; set; }
        [JsonPropertyName("internal")] public bool Internal { get; set; }
        [JsonPropertyName("auto_delete")] public bool AutoDelete { get; set; }
    }
}