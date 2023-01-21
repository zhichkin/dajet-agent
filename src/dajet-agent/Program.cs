using DaJet.Agent.Consumer;
using DaJet.Agent.Kafka.Consumer;
using DaJet.Agent.Kafka.Producer;
using DaJet.Agent.Producer;
using DaJet.Logging;
using DaJet.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DaJet.Agent.Service
{
    public static class Program
    {
        private const string LOG_TOKEN = "HOST";
        private static AppSettings AppSettings { get; set; } = new AppSettings();
        public static void Main()
        {
            InitializeAppSettings();

            FileLogger.UseLogSize(AppSettings.LogSize);
            FileLogger.UseCatalog(AppSettings.AppCatalog);
            FileLogger.UseFileName("dajet-agent");

            FileLogger.Log("Hosting service is started.");
            CreateHostBuilder().Build().Run();
            FileLogger.Log("Hosting service is stopped.");
        }
        private static void InitializeAppSettings()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string catalogPath = Path.GetDirectoryName(asm.Location);

            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(catalogPath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            config.Bind(AppSettings);

            AppSettings.AppCatalog = catalogPath;

            if (AppSettings.ExchangePlans == null || AppSettings.ExchangePlans.Count == 0)
            {
                AppSettings.ExchangePlans = new List<string>() { "ѕланќбмена.ѕланќбменаƒанными" };
            }

            ServiceCollection services = new();
            services.AddDataProtection();
            services.AddOptions().AddSingleton(Options.Create(AppSettings));
            IServiceProvider provider = services.BuildServiceProvider();
            SqliteAppSettingsProvider settings = ActivatorUtilities.CreateInstance<SqliteAppSettingsProvider>(provider);
            settings.Configure();
        }
        private static IHostBuilder CreateHostBuilder()
        {
            IHostBuilder builder = Host.CreateDefaultBuilder()
                .UseSystemd()
                .UseWindowsService()
                .ConfigureAppConfiguration(config =>
                {
                    config.Sources.Clear();
                    config
                        .SetBasePath(AppSettings.AppCatalog)
                        .AddJsonFile("appsettings.json", optional: false);
                })
                .ConfigureServices(ConfigureServices);

            builder.ConfigureWebHostDefaults(webHost =>
            {
                webHost.UseKestrel()
                .UseStartup<WebServerStartup>()
                .UseWebRoot("ui")
                .UseContentRoot(AppContext.BaseDirectory);
            });

            return builder;
        }
        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services
                .AddOptions()
                .AddSingleton(Options.Create(AppSettings))
                .Configure<HostOptions>(context.Configuration.GetSection(nameof(HostOptions)));
            services.AddDataProtection();
            services.AddSingleton<SqliteAppSettingsProvider>();

            services.AddMemoryCache();
            services.AddHostedService<MetadataCacheService>();
            services.AddSingleton<IMetadataCache, MetadataCache>();

            ConfigureDaJetAgentOptions(services);

            if (AppSettings.UseProducer || AppSettings.UseConsumer)
            {
                services.AddHostedService<RabbitMQConfigurator>();
            }

            if (AppSettings.UseDeliveryTracking)
            {
                services.AddHostedService<DeliveryTrackingService>();
            }

            if (!string.IsNullOrWhiteSpace(AppSettings.MonitorQueueName))
            {
                services.AddHostedService<DeliveryTrackingConsumer>();
            }

            if (AppSettings.UseProducer)
            {
                ConfigureProducerSettings(services);
                services.AddHostedService<MessageProducerService>();
            }

            if (AppSettings.UseConsumer)
            {
                ConfigureConsumerSettings(services);
                services.AddHostedService<MessageConsumerService>();
            }

            if (AppSettings.Kafka.Producer.IsEnabled)
            {
                services.AddHostedService<KafkaProducerService>();
            }

            if (AppSettings.Kafka.Consumer.IsEnabled)
            {
                services.AddHostedService<KafkaConsumerService>();
            }
        }
        private static void ConfigureDaJetAgentOptions(IServiceCollection services)
        {
            MessageProducerSettings settings = new MessageProducerSettings();

            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppSettings.AppCatalog)
                .AddJsonFile("producer-settings.json", optional: false)
                .Build();

            config.Bind(settings);

            DaJetAgentOptions options = new DaJetAgentOptions()
            {
                DatabaseProvider = settings.DatabaseSettings.DatabaseProvider,
                ConnectionString = settings.DatabaseSettings.ConnectionString,
                HostName = settings.MessageBrokerSettings.HostName,
                VirtualHost = settings.MessageBrokerSettings.VirtualHost,
                PortNumber = settings.MessageBrokerSettings.PortNumber,
                UserName = settings.MessageBrokerSettings.UserName,
                Password = settings.MessageBrokerSettings.Password,
                ExchangeRole = (settings.MessageBrokerSettings.ExchangeRole == 0
                ? ExchangeRoles.Aggregator
                : ExchangeRoles.Dispatcher),
                UseDeliveryTracking = AppSettings.UseDeliveryTracking
            };

            services.AddSingleton(Options.Create(options));
        }
        private static void ConfigureProducerSettings(IServiceCollection services)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppSettings.AppCatalog)
                .AddJsonFile("producer-settings.json", optional: false)
                .Build();
            services.Configure<MessageProducerSettings>(config);
        }
        private static void ConfigureConsumerSettings(IServiceCollection services)
        {
           IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppSettings.AppCatalog)
                .AddJsonFile("consumer-settings.json", optional: false)
                .Build();
            services.Configure<MessageConsumerSettings>(config);
        }
    }
}