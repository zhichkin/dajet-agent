using System.Text.Json.Serialization;

namespace DaJet.Telegram.Bot
{
    internal sealed class SendMessageRequest
    {
        [JsonPropertyName("chat_id")] public string Chat { get; set; }
        [JsonPropertyName("text")] public string Text { get; set; }
    }
}