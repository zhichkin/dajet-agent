using DaJet.Metadata;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

namespace DaJet.Agent.Service
{
    public sealed class AppSettings
    {
        public int LogSize { get; set; } = 1024 * 1024; // kilobytes
        public bool UseProducer { get; set; } = true;
        public bool UseConsumer { get; set; } = true;
        public bool UseDeliveryTracking { get; set; } = false;
        public string AppCatalog { get; set; } = string.Empty;
        public int RefreshTimeout { get; set; } = 600; // seconds
        public List<string> ExchangePlans { get; set; } = new List<string>();
        internal string ThisNode { get; set; } = string.Empty;
        public string MonitorQueueName { get; set; } = string.Empty;
        public string MessageBrokerUri { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        internal DatabaseProvider DatabaseProvider { get; set; } = DatabaseProvider.SQLServer;
        public KafkaSettings Kafka { get; set; } = new KafkaSettings();
    }
    public sealed class KafkaSettings
    {
        public KafkaProducerSettings Producer { get; set; } = new KafkaProducerSettings();
    }
    public sealed class KafkaProducerSettings
    {
        public bool IsEnabled { get; set; } = false;
        public int IdleDelay { get; set; } = 60; // seconds
        public int ErrorDelay { get; set; } = 300; // seconds
        public string ConnectionString { get; set; } = string.Empty;
        public string OutgoingQueueName { get; set; } = string.Empty;
        [JsonIgnore] public Assembly EntityModel { get; set; } = null;
        public string ModelAssemblyName { get; set; } = string.Empty;
        public string DefaultTopic { get; set; } = string.Empty;
        public Dictionary<string, string> Topics { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();
    }
}