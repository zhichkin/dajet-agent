using DaJet.Agent.Consumer;
using DaJet.Agent.Producer;
using DaJet.Agent.Service.Services;
using DaJet.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;

namespace DaJet.Agent.Service
{
    public static class Program
    {
        private static IHost _host;
        private const string LOG_TOKEN = "HOST";
        private static AppSettings AppSettings { get; set; }
        private static IConfigurationRoot Config { get; set; }
        public static void Main()
        {
            FileLogger.Log(LOG_TOKEN, "Hosting service is started.");

            InitializeAppSettings();

            _host = CreateHostBuilder().Build();
            _host.Run();
            
            FileLogger.Log(LOG_TOKEN, "Hosting service is stopped.");
        }
        private static void InitializeAppSettings()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string catalogPath = Path.GetDirectoryName(asm.Location);

            AppSettings = new AppSettings()
            {
                AppCatalog = catalogPath
            };

            Config = new ConfigurationBuilder()
                .SetBasePath(AppSettings.AppCatalog)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            Config.Bind(AppSettings);
        }
        private static IHostBuilder CreateHostBuilder()
        {
            IHostBuilder builder = Host.CreateDefaultBuilder()
                .UseWindowsService()
                .ConfigureServices(ConfigureServices);

            if (AppSettings.UseWebServer)
            {
                builder.ConfigureWebHostDefaults(web =>
                {
                    web
                    .UseKestrel()
                    .UseStartup<Startup>();
                });
            }
            
            return builder;
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            ConfigureAppSettings(services);

            if (AppSettings.UseProducer)
            {
                services.AddSingleton<IMessageProducer, MessageProducer>();
                services.AddSingleton<IDatabaseMessageConsumer, DatabaseMessageConsumer>();
                services.AddHostedService<MessageProducerService>();
            }
            if (AppSettings.UseConsumer)
            {
                services.AddSingleton<IMessageConsumer, MessageConsumer>();
                services.AddSingleton<IDatabaseMessageProducer, DatabaseMessageProducer>();
                services.AddHostedService<MessageConsumerService>();
            }
            services.AddSingleton<IPubSubService, PubSubService>();
        }
        private static void ConfigureAppSettings(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AppSettings>(Config);
            services.Configure<HostOptions>(Config.GetSection("HostOptions"));

            FileLogger.LogSize = AppSettings.LogSize;
            
            ConfigureProducerSettings(services, AppSettings.AppCatalog);
            ConfigureConsumerSettings(services, AppSettings.AppCatalog);
        }
        private static void ConfigureProducerSettings(IServiceCollection services, string catalogPath)
        {
            MessageProducerSettings settings = new MessageProducerSettings();
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(catalogPath)
                .AddJsonFile("producer-settings.json", optional: false)
                .Build();
            config.Bind(settings);
            services.Configure<MessageProducerSettings>(config);
        }
        private static void ConfigureConsumerSettings(IServiceCollection services, string catalogPath)
        {
            MessageConsumerSettings settings = new MessageConsumerSettings();
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(catalogPath)
                .AddJsonFile("consumer-settings.json", optional: false)
                .Build();
            config.Bind(settings);
            services.Configure<MessageConsumerSettings>(config);
        }
    }
}