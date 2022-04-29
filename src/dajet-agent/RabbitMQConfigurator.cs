using DaJet.Logging;
using DaJet.Metadata.Model;
using DaJet.RabbitMQ;
using DaJet.RabbitMQ.HttpApi;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DaJet.Agent.Service
{
    internal sealed class RabbitMQConfigurator : BackgroundService
    {
        private const string SHUTDOWN_MESSAGE = "RabbitMQ configurator shutdown.";

        private CancellationToken _cancellationToken;
        private readonly IMetadataCache _metadataCache;
        private AppSettings Settings { get; set; }
        private DaJetAgentOptions Options { get; set; }
        public RabbitMQConfigurator(IOptions<AppSettings> settings, IOptions<DaJetAgentOptions> options, IMetadataCache cache)
        {
            _metadataCache = cache;
            Options = options.Value;
            Settings = settings.Value;
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;

            return Task.Factory.StartNew(DoWork, TaskCreationOptions.LongRunning);
        }
        private void DoWork()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                try
                {
                    TryDoWork();

                    Task.Delay(TimeSpan.FromSeconds(Settings.RefreshTimeout)).Wait(_cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    // do nothing - the wait task has been canceled
                }
                catch (Exception error)
                {
                    FileLogger.LogException(error);
                }
            }
            FileLogger.Log(SHUTDOWN_MESSAGE);
        }
        private void TryDoWork()
        {
            if (Options.ExchangeRole != ExchangeRoles.Dispatcher)
            {
                return;
            }

            if (Settings.ExchangePlans == null || Settings.ExchangePlans.Count == 0)
            {
                Settings.ExchangePlans = new List<string>() { "ПланОбмена.ПланОбменаДанными" };
            }
            
            GetExchangePlanSettingsWithRetry(out string thisNode, out List<string> exchangeNodes);

            FileLogger.Log($"This node is [{thisNode}]");

            if (exchangeNodes.Count == 0)
            {
                FileLogger.Log("None exchange node is found.");
                return;
            }

            using (IRabbitMQHttpManager manager = new RabbitMQHttpManager()
                .UseHostName(Options.HostName)
                .UseVirtualHost(Options.VirtualHost)
                .UseUserName(Options.UserName)
                .UsePassword(Options.Password))
            {
                bool error = false;

                if (!TryGetDispatcherTopic(in manager, out ExchangeInfo dispatcher))
                {
                    error = true;
                    FileLogger.Log($"Dispatcher topic \"{Options.DispatcherTopic}\" is not found.");
                }

                if (!TryGetAggregatorTopic(in manager, out ExchangeInfo aggregator))
                {
                    error = true;
                    FileLogger.Log($"Aggregator topic \"{Options.AggregatorTopic}\" is not found.");
                }

                if (!error)
                {
                    ConfigureDispatcherQueues(in manager, in dispatcher, in thisNode, in exchangeNodes);
                    ConfigureAggregatorQueues(in manager, in aggregator, in thisNode, in exchangeNodes);
                }
            }
        }
        private void GetExchangePlanSettingsWithRetry(out string thisNode, out List<string> exchangeNodes)
        {
            while (true)
            {
                try
                {
                    if (!_metadataCache.TryGet(out InfoBase infoBase))
                    {
                        throw new Exception("Failed to get metadata from cache.");
                    }

                    ExchangePlanHelper settings = new ExchangePlanHelper(
                        in infoBase,
                        Options.DatabaseProvider,
                        Options.ConnectionString);

                    settings.ConfigureSelectScripts(Settings.ExchangePlans[0]);
                    thisNode = settings.GetThisNode();
                    exchangeNodes = settings.GetExchangeNodes();

                    return;
                }
                catch (Exception error)
                {
                    FileLogger.Log("Failed to get exchange plan settings.");
                    FileLogger.LogException(error);
                }

                Task.Delay(TimeSpan.FromSeconds(Settings.RefreshTimeout)).Wait(_cancellationToken);
            }
        }
        private bool TryGetDispatcherTopic(in IRabbitMQHttpManager manager, out ExchangeInfo exchange)
        {
            exchange = manager.GetExchange(Options.DispatcherTopic).Result;

            return (exchange != null);
        }
        private bool TryGetAggregatorTopic(in IRabbitMQHttpManager manager, out ExchangeInfo exchange)
        {
            exchange = manager.GetExchange(Options.AggregatorTopic).Result;

            return (exchange != null);
        }
        private void ConfigureDispatcherQueues(in IRabbitMQHttpManager manager, in ExchangeInfo dispatcher, in string thisNode, in List<string> exchangeNodes)
        {
            foreach (string exchangeNode in exchangeNodes)
            {
                string queueName = $"РИБ.{thisNode}.{exchangeNode}";

                QueueInfo queue = manager.GetQueue(queueName).Result;

                if (queue == null)
                {
                    manager.CreateQueue(queueName).Wait();
                    queue = manager.GetQueue(queueName).Result;
                    FileLogger.Log($"Queue [{queue.Name}] created successfully.");
                }

                List<BindingInfo> bindings = manager.GetBindings(queue).Result;

                bool exists = false;

                foreach (BindingInfo binding in bindings)
                {
                    if (binding.Source == Options.DispatcherTopic)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    manager.CreateBinding(dispatcher, queue, exchangeNode).Wait();

                    FileLogger.Log($"Binding from [{dispatcher.Name}] to [{queue.Name}] with routing key [{exchangeNode}] created successfully.");
                }
            }
        }
        private void ConfigureAggregatorQueues(in IRabbitMQHttpManager manager, in ExchangeInfo aggregator, in string thisNode, in List<string> exchangeNodes)
        {
            foreach (string exchangeNode in exchangeNodes)
            {
                string queueName = $"РИБ.{exchangeNode}.{thisNode}";

                QueueInfo queue = manager.GetQueue(queueName).Result;

                if (queue == null)
                {
                    manager.CreateQueue(queueName).Wait();
                    queue = manager.GetQueue(queueName).Result;
                    FileLogger.Log($"Queue [{queue.Name}] created successfully.");
                }

                List<BindingInfo> bindings = manager.GetBindings(queue).Result;

                bool exists = false;

                foreach (BindingInfo binding in bindings)
                {
                    if (binding.Source == Options.AggregatorTopic)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    manager.CreateBinding(aggregator, queue, exchangeNode).Wait();

                    FileLogger.Log($"Binding from [{aggregator.Name}] to [{queue.Name}] with routing key [{exchangeNode}] created successfully.");
                }
            }
        }
    }
}