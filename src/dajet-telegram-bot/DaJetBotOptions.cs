namespace DaJet.Telegram.Bot
{
    public sealed class DaJetBotOptions
    {
        public string Token { get; set; } = string.Empty;
        public string Channel { get; set; } = string.Empty;
        public string ApiUrl { get; set; } = "https://api.telegram.org";
    }
}