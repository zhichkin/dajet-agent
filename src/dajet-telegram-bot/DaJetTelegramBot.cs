using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DaJet.Telegram.Bot
{
    // https://core.telegram.org/bots/api
    public interface IDaJetTelegramBot : IDisposable
    {
        void SendMessage(string message);
        Task SendMessageAsync(string message);
    }
    public sealed class DaJetTelegramBot : IDaJetTelegramBot
    {
        private readonly DaJetBotOptions _options;
        private HttpClient HttpClient { get; set; } = new HttpClient();
        public DaJetTelegramBot(IOptions<DaJetBotOptions> options)
        {
            _options = options.Value;

            HttpClient.BaseAddress = new Uri(_options.ApiUrl);
        }
        public void Dispose()
        {
            HttpClient?.Dispose();
            HttpClient = null;
        }
        public void SendMessage(string message)
        {
            string url = $"/bot{_options.Token}/sendMessage";

            SendMessageRequest request = new SendMessageRequest()
            {
                Chat = _options.Channel,
                Text = message
            };

            Task<HttpResponseMessage> task = HttpClient.PostAsJsonAsync(url, request);

            if (!task.Wait(TimeSpan.FromSeconds(5)))
            {
                throw new TimeoutException();
            }
            else
            {
                HttpResponseMessage response = task.Result;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"{(int)response.StatusCode}: {response.ReasonPhrase}");
                }
            }
        }
        public async Task SendMessageAsync(string message)
        {
            string url = $"/bot{_options.Token}/sendMessage";

            SendMessageRequest request = new SendMessageRequest()
            {
                Chat = _options.Channel,
                Text = message
            };

            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(url, request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"{(int)response.StatusCode}: {response.ReasonPhrase}");
            }
        }
    }
}