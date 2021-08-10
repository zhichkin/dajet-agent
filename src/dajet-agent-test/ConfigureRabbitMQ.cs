using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Management.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaJet.Agent.Test
{
    [TestClass] public sealed class ConfigureRabbitMQ
    {
        private readonly IRabbitHttpManager manager = new RabbitHttpManager();
        public ConfigureRabbitMQ()
        {
            manager
                .UseHostName("localhost")
                .UseUserName("guest")
                .UsePassword("guest");
        }
        [TestMethod] public async Task CreateExchanges()
        {
            string[] exchanges = new string[] { "РИБ.ERP", "РИБ.АПО" };

            foreach (string exchangeName in exchanges)
            {
                ExchangeInfo exchange = await manager.GetExchange(exchangeName);

                if (exchange == null)
                {
                    try
                    {
                        await manager.CreateExchange(exchangeName);

                        exchange = await manager.GetExchange(exchangeName);

                        Console.WriteLine($"Exchange \"{exchange.Name}\" [{exchange.Type}] ({(exchange.Durable ? "durable" : "transient")}) created successfully.");
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine($"Failed to create exchange \"{exchange.Name}\":");
                        Console.WriteLine(error.Message);
                    }
                }
                else
                {
                    Console.WriteLine($"Exchange \"{exchange.Name}\" [{exchange.Type}] ({(exchange.Durable ? "durable" : "transient")}) already exists.");
                }
            }
        }
        [TestMethod] public async Task BindQueuesToAggregator()
        {
            string exchangeName = "РИБ.АПО";
            string queueFilter = @"^РИБ[.][0-9]+[.]ЦБ$";

            ExchangeInfo exchange = await manager.GetExchange(exchangeName);
            if (exchange == null)
            {
                Console.WriteLine($"Exchange \"{exchangeName}\" is not found.");
                return;
            }

            int page = 1;
            int size = 100;
            QueueResponse response = await manager.GetQueues(page, size, queueFilter);
            if (response == null || response.FilteredCount == 0)
            {
                Console.WriteLine("Queues are not found.");
                return;
            }
            int pageCount = response.PageCount;

            while (page <= pageCount)
            {
                Console.WriteLine($"Page # {response.Page} of total {response.PageCount} pages (size = {response.PageSize})");
                Console.WriteLine($"Items count: {response.ItemCount}");
                Console.WriteLine($"Total items count: {response.TotalCount}");
                Console.WriteLine($"Filtered items count: {response.FilteredCount}");

                for (int i = 0; i < response.Items.Count; i++)
                {
                    QueueInfo queue = response.Items[i];

                    Console.WriteLine($"{(i + 1)}. Queue \"{queue.Name}\" ({(queue.Durable ? "durable" : "transient")})");

                    List<BindingInfo> bindings = await manager.GetBindings(queue);

                    bool exists = false;

                    foreach (BindingInfo binding in bindings)
                    {
                        if (binding.Source == exchangeName)
                        {
                            exists = true;
                            Console.WriteLine($" - [{binding.Source}] -> [{binding.Destination}] ({binding.RoutingKey}) {binding.PropertiesKey}");
                            break;
                        }
                    }

                    if (!exists)
                    {
                        string routingKey = queue.Name.Split('.')[1];
                        await manager.CreateBinding(exchange, queue, routingKey);
                        Console.WriteLine($" + [{exchange.Name}] -> [{queue.Name}] ({routingKey})");
                    }
                }

                page++;

                if (page <= pageCount)
                {
                    response = await manager.GetQueues(page, size, queueFilter);
                }
            }
        }
        [TestMethod] public async Task BindQueuesToDistributor()
        {
            string exchangeName = "РИБ.ERP";
            string queueFilter = @"^РИБ[.]ЦБ[.][0-9]+$";

            ExchangeInfo exchange = await manager.GetExchange(exchangeName);
            if (exchange == null)
            {
                Console.WriteLine($"Exchange \"{exchangeName}\" is not found.");
                return;
            }

            int page = 1;
            int size = 100;
            QueueResponse response = await manager.GetQueues(page, size, queueFilter);
            if (response == null || response.FilteredCount == 0)
            {
                Console.WriteLine("Queues are not found.");
                return;
            }
            int pageCount = response.PageCount;

            while (page <= pageCount)
            {
                Console.WriteLine($"Page # {response.Page} of total {response.PageCount} pages (size = {response.PageSize})");
                Console.WriteLine($"Items count: {response.ItemCount}");
                Console.WriteLine($"Total items count: {response.TotalCount}");
                Console.WriteLine($"Filtered items count: {response.FilteredCount}");

                for (int i = 0; i < response.Items.Count; i++)
                {
                    QueueInfo queue = response.Items[i];

                    Console.WriteLine($"{(i + 1)}. Queue \"{queue.Name}\" ({(queue.Durable ? "durable" : "transient")})");

                    List<BindingInfo> bindings = await manager.GetBindings(queue);

                    bool exists = false;

                    foreach (BindingInfo binding in bindings)
                    {
                        if (binding.Source == exchangeName)
                        {
                            exists = true;
                            Console.WriteLine($" - [{binding.Source}] -> [{binding.Destination}] ({binding.RoutingKey}) {binding.PropertiesKey}");
                            break;
                        }
                    }

                    if (!exists)
                    {
                        string routingKey = queue.Name.Split('.')[2];
                        await manager.CreateBinding(exchange, queue, routingKey);
                        Console.WriteLine($" + [{exchange.Name}] -> [{queue.Name}] ({routingKey})");
                    }
                }

                page++;

                if (page <= pageCount)
                {
                    response = await manager.GetQueues(page, size, queueFilter);
                }
            }
        }
    }
}