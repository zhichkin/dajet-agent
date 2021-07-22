using DaJet.Agent.Model;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DaJet.Agent.Blazor
{
    public interface IPublicationViewModel
    {
        bool IsInitialized { get; }
        Task InitializeViewModel(int publisher, int publication);
        int Id { get; }
        Node Publisher { get; }
        string Name { get; set; }
        string GetTitle();
        string GetPublicationsLink();
        string GetPublisherPresentation();
        Task SaveChanges();
    }
    public sealed class PublicationViewModel : IPublicationViewModel
    {
        private Node _publisher;
        private Publication _publication;
        private HttpClient Http { get; }
        public PublicationViewModel(HttpClient http)
        {
            Http = http;
        }
        public async Task InitializeViewModel(int publisher, int publication)
        {
            _publisher = await Http.GetFromJsonAsync<Node>($"dajet/nodes/{publisher}");

            if (publication == 0)
            {
                _publication = new Publication()
                {
                    Publisher = _publisher.Id
                };
            }
            else
            {
                _publication = await Http.GetFromJsonAsync<Publication>($"dajet/publications/{publication}");
            }

            IsInitialized = true;
        }
        public bool IsInitialized { get; private set; }
        public int Id { get { return _publication.Id; } }
        public string Name { get { return _publication.Name; } set { _publication.Name = value; } }
        public Node Publisher { get { return _publisher; } }

        public string GetTitle()
        {
            return (Id > 0) ? Id.ToString() : "Новая";
        }
        public string GetPublicationsLink()
        {
            return $"publications/{Publisher.Id}";
        }
        public string GetPublisherPresentation()
        {
            return $"[{Publisher.Id}] {Publisher.Code} ({Publisher.Description})";
        }
        public async Task SaveChanges()
        {
            if (_publication.Id == 0)
            {
                var response = await Http.PostAsJsonAsync("dajet/publications", _publication);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    _publication = JsonSerializer.Deserialize<Publication>(json);
                }
                else
                {
                    // TODO: throw exception
                }
            }
            else
            {
                var response = await Http.PutAsJsonAsync("dajet/publications", _publication);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    // TODO: throw exception
                }
            }
        }
    }
}