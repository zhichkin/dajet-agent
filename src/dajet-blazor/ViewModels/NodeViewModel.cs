using DaJet.Agent.Model;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DaJet.Agent.Blazor
{
    public interface INodeViewModel
    {
        bool IsInitialized { get; }
        Task InitializeViewModel(int id);
        int Id { get; }
        string Code { get; set; }
        string Description { get; set; }
        string GetTitle();
        Task SaveChanges();
    }
    public sealed class NodeViewModel : INodeViewModel
    {
        private Node _node;
        private HttpClient Http { get; }
        public NodeViewModel(HttpClient http)
        {
            Http = http;
        }
        public async Task InitializeViewModel(int id)
        {
            if (id == 0)
            {
                _node = new Node();
            }
            else
            {
                _node = await Http.GetFromJsonAsync<Node>($"dajet/nodes/{id}");
            }
            IsInitialized = (_node != null);
        }
        public bool IsInitialized { get; private set; }
        public int Id { get { return _node.Id; } }
        public string Code { get { return _node.Code; } set { _node.Code = value; } }
        public string Description { get { return _node.Description; } set { _node.Description = value; } }

        public string GetTitle()
        {
            return (Id > 0) ? Id.ToString() : "*";
        }
        public async Task SaveChanges()
        {
            if (_node.Id == 0)
            {
                var response = await Http.PostAsJsonAsync("dajet/nodes", _node);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    _node = JsonSerializer.Deserialize<Node>(json);
                }
                else
                {
                    // TODO: throw exception
                }
            }
            else
            {
                var response = await Http.PutAsJsonAsync("dajet/nodes", _node);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    // TODO: throw exception
                }
            }
        }
    }
}