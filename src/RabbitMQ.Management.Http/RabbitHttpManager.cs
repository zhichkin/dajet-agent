using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

// https://rawcdn.githack.com/rabbitmq/rabbitmq-server/v3.8.19/deps/rabbitmq_management/priv/www/api/index.html

namespace RabbitMQ.Management.Http
{
    public interface IRabbitHttpManager
    {
        #region "Configure HTTP connection"

        IRabbitHttpManager UseHostName(string hostName);
        IRabbitHttpManager UsePortNumber(int portNumber);
        IRabbitHttpManager UseVirtualHost(string vhost);
        IRabbitHttpManager UseUserName(string userName);
        IRabbitHttpManager UsePassword(string password);

        string HostName { get; }
        int PortNumber { get; }
        string VirtualHost { get; }
        string UserName { get; }

        #endregion

        Task CreateExchange(string name);
        Task DeleteExchange(string name);
        Task<List<ExchangeInfo>> GetExchanges();
        Task<ExchangeInfo> GetExchange(string name);

        Task<List<QueueInfo>> GetQueues();
        Task<QueueResponse> GetQueues(int page, int size, string regex);
        Task<QueueInfo> GetQueue(string name);
        Task CreateQueue(string name);
        Task DeleteQueue(string name);

        Task<List<BindingInfo>> GetBindings(QueueInfo queue);
        Task CreateBinding(ExchangeInfo exchange, QueueInfo queue, string routingKey);
        Task DeleteBinding(BindingInfo binding);
    }
    public sealed class RabbitHttpManager : IRabbitHttpManager
    {
        private HttpClient HttpClient { get; set; } = new HttpClient();
        public RabbitHttpManager() { ConfigureHttpClient(); }

        #region "HttpClient configuration"
        
        private void ConfigureHttpClient()
        {
            HttpClient.BaseAddress = new Uri($"http://{HostName}:{PortNumber}");
            byte[] authToken = Encoding.ASCII.GetBytes($"{UserName}:{Password}");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
        }

        public string HostName { get; private set; } = "localhost";
        public int PortNumber { get; private set; } = 15672;
        public string VirtualHost { get; private set; } = "/";
        public string UserName { get; private set; } = "guest";
        private string Password { get; set; } = "guest";

        public IRabbitHttpManager UseHostName(string hostName)
        {
            HostName = hostName;
            ConfigureHttpClient();
            return this;
        }
        public IRabbitHttpManager UsePortNumber(int portNumber)
        {
            PortNumber = portNumber;
            ConfigureHttpClient();
            return this;
        }
        public IRabbitHttpManager UseVirtualHost(string vhost)
        {
            VirtualHost = vhost;
            return this;
        }
        public IRabbitHttpManager UseUserName(string userName)
        {
            UserName = userName;
            ConfigureHttpClient();
            return this;
        }
        public IRabbitHttpManager UsePassword(string password)
        {
            Password = password;
            ConfigureHttpClient();
            return this;
        }

        #endregion

        public async Task<List<ExchangeInfo>> GetExchanges()
        {
            string url = $"/api/exchanges/{HttpUtility.UrlEncode(VirtualHost)}";
            HttpResponseMessage response = await HttpClient.GetAsync(url);
            Stream stream = await response.Content.ReadAsStreamAsync();
            List<ExchangeInfo> list = await JsonSerializer.DeserializeAsync<List<ExchangeInfo>>(stream);
            return list;
        }
        public async Task<ExchangeInfo> GetExchange(string name)
        {
            string url = $"/api/exchanges/{HttpUtility.UrlEncode(VirtualHost)}?page=1&page_size=1&name={name}";
            HttpResponseMessage response = await HttpClient.GetAsync(url);
            Stream stream = await response.Content.ReadAsStreamAsync();
            ExchangeResponse exchanges = await JsonSerializer.DeserializeAsync<ExchangeResponse>(stream);
            return (exchanges == null || exchanges.Items == null || exchanges.Items.Count == 0)
                ? null
                : exchanges.Items[0];
        }
        public async Task CreateExchange(string name)
        {
            string url = $"/api/exchanges/{HttpUtility.UrlEncode(VirtualHost)}/{name}";
            ExchangeRequest exchange = new ExchangeRequest()
            {
                Type = "topic",
                Durable = true,
                Internal = false,
                AutoDelete = false
            };
            HttpResponseMessage response = await HttpClient.PutAsJsonAsync(url, exchange);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception(response.ReasonPhrase); // No Content
            }
        }
        public async Task DeleteExchange(string name)
        {
            string url = $"/api/exchanges/{HttpUtility.UrlEncode(VirtualHost)}/{name}";
            HttpResponseMessage response = await HttpClient.DeleteAsync(url);
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new Exception(response.ReasonPhrase); // Not Found
            }
            // When DELETEing an exchange you can add the query string parameter if-unused = true.
            // This prevents the delete from succeeding if the exchange is bound to a queue or as a source to another exchange.
        }

        public async Task<List<QueueInfo>> GetQueues()
        {
            string url = $"/api/queues/{HttpUtility.UrlEncode(VirtualHost)}";
            HttpResponseMessage response = await HttpClient.GetAsync(url);
            Stream stream = await response.Content.ReadAsStreamAsync();
            List<QueueInfo> list = await JsonSerializer.DeserializeAsync<List<QueueInfo>>(stream);
            return list;
        }
        public async Task<QueueResponse> GetQueues(int page, int size, string regex)
        {
            if (size == 0 || size > 100)
            {
                size = 100;
            }
            string url = $"/api/queues/{HttpUtility.UrlEncode(VirtualHost)}?page={page}&page_size={size}&name={HttpUtility.UrlEncode(regex)}&use_regex=true";
            HttpResponseMessage response = await HttpClient.GetAsync(url);
            Stream stream = await response.Content.ReadAsStreamAsync();
            QueueResponse result = await JsonSerializer.DeserializeAsync<QueueResponse>(stream);
            return result;
        }
        public async Task<QueueInfo> GetQueue(string name)
        {
            string url = $"/api/queues/{HttpUtility.UrlEncode(VirtualHost)}?page=1&page_size=1&name={name}";
            HttpResponseMessage response = await HttpClient.GetAsync(url);
            Stream stream = await response.Content.ReadAsStreamAsync();
            QueueResponse queues = await JsonSerializer.DeserializeAsync<QueueResponse>(stream);
            return (queues == null || queues.Items == null || queues.Items.Count == 0)
                ? null
                : queues.Items[0];
        }
        public async Task CreateQueue(string name)
        {
            string url = $"/api/queues/{HttpUtility.UrlEncode(VirtualHost)}/{name}";
            QueueRequest queue = new QueueRequest()
            {
                Durable = true
            };
            HttpResponseMessage response = await HttpClient.PutAsJsonAsync(url, queue);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception(response.ReasonPhrase); // No Content
            }
        }
        public async Task DeleteQueue(string name)
        {
            string url = $"/api/queues/{HttpUtility.UrlEncode(VirtualHost)}/{name}";
            HttpResponseMessage response = await HttpClient.DeleteAsync(url);
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new Exception(response.ReasonPhrase); // Not Found
            }
            // When DELETEing a queue you can add the query string parameters if-empty=true and / or if-unused=true.
            // These prevent the delete from succeeding if the queue contains messages, or has consumers, respectively.
        }

        public async Task<List<BindingInfo>> GetBindings(QueueInfo queue)
        {
            string url = $"/api/queues/{HttpUtility.UrlEncode(VirtualHost)}/{queue.Name}/bindings";
            HttpResponseMessage response = await HttpClient.GetAsync(url);
            Stream stream = await response.Content.ReadAsStreamAsync();
            List<BindingInfo> list = await JsonSerializer.DeserializeAsync<List<BindingInfo>>(stream);
            return list;
        }
        public async Task CreateBinding(ExchangeInfo exchange, QueueInfo queue, string routingKey)
        {
            string url = $"/api/bindings/{HttpUtility.UrlEncode(VirtualHost)}/e/{exchange.Name}/q/{queue.Name}";
            BindingRequest binding = new BindingRequest()
            {
                RoutingKey = (routingKey == null ? string.Empty : routingKey)
            };
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(url, binding);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception(response.ReasonPhrase); // No Content
            }
            else
            {
                Uri location = response.Headers.Location;
            }
        }
        public async Task DeleteBinding(BindingInfo binding)
        {
            string url = $"/api/bindings/{HttpUtility.UrlEncode(VirtualHost)}/e/{binding.Source}/q/{binding.Destination}/{binding.PropertiesKey}";
            HttpResponseMessage response = await HttpClient.DeleteAsync(url);
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new Exception(response.ReasonPhrase); // Not Found
            }
        }
    }
}