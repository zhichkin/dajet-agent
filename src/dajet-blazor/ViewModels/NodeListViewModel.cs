using DaJet.Agent.Model;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DaJet.Agent.Blazor
{
    public interface INodeListViewModel
    {
        Task InitializeViewModel();
        List<Node> Nodes { get; }
        string SearchText { get; set; }
        Task Refresh();
        Task DeleteNode(int id);
    }
    public sealed class NodeListViewModel : INodeListViewModel
    {
        private HttpClient Http { get; }
        public NodeListViewModel(HttpClient http)
        {
            Http = http;
        }
        public async Task InitializeViewModel()
        {
            _nodes = await Http.GetFromJsonAsync<List<Node>>("dajet/nodes");
        }
        private List<Node> _nodes;
        public List<Node> Nodes
        {
            get
            {
                if (_nodes == null) return _nodes;

                if (string.IsNullOrWhiteSpace(_searchText)
                    || string.IsNullOrWhiteSpace(_searchText.Trim()))
                {
                    return _nodes;
                }
                else
                {
                    return _nodes.Where(n => n.Code.Contains(_searchText.Trim())).ToList();
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
            _nodes = null;
            await InitializeViewModel();
        }
        public async Task DeleteNode(int id)
        {
            await Http.DeleteAsync($"dajet/nodes/{id}");
            await InitializeViewModel();
        }
    }
}