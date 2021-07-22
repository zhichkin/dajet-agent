using DaJet.Agent.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DaJet.Agent.Blazor
{
    public interface IPublicationListViewModel
    {
        bool IsInitialized { get; }
        Task InitializeViewModel(int publisher);
        Node Publisher { get; }
        List<Publication> Publications { get; }
        string SearchText { get; set; }
        Task Refresh();
        Task DeletePublication(int id);
        string GetNewPublicationLink();
        string GetPublisherPresentation();
    }
    public sealed class PublicationListViewModel : IPublicationListViewModel
    {
        private HttpClient Http { get; }
        public PublicationListViewModel(HttpClient http)
        {
            Http = http;
        }
        public async Task InitializeViewModel(int publisher)
        {
            Publisher = await Http.GetFromJsonAsync<Node>($"dajet/nodes/{publisher}");
            _publications = await Http.GetFromJsonAsync<List<Publication>>($"dajet/publications/publisher/{publisher}");

            IsInitialized = true;
        }
        public bool IsInitialized { get; private set; }
        public Node Publisher { get; private set; }
        private List<Publication> _publications;
        public List<Publication> Publications
        {
            get
            {
                if (_publications == null) return _publications;

                if (string.IsNullOrWhiteSpace(_searchText)
                    || string.IsNullOrWhiteSpace(_searchText.Trim()))
                {
                    return _publications;
                }
                else
                {
                    return _publications.Where(n => n.Name.Contains(_searchText.Trim())).ToList();
                }
            }
        }
        private string _searchText = string.Empty;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
            }
        }

        public async Task Refresh()
        {
            IsInitialized = false;
            await InitializeViewModel(Publisher.Id);
        }
        public async Task DeletePublication(int id)
        {
            await Http.DeleteAsync($"dajet/publications/{id}");
            await InitializeViewModel((Publisher == null ? 0 : Publisher.Id));
        }
        public string GetPublisherPresentation()
        {
            return $"[{Publisher.Id}] {Publisher.Code} ({Publisher.Description})";
        }
        public string GetNewPublicationLink()
        {
            return $"publisher/{Publisher.Id}/publication";
        }
    }
}