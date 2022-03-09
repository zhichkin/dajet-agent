using DaJet.Agent.Consumer;
using DaJet.Agent.Producer;
using DaJet.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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
            
            return builder;
        }
        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services
                .AddOptions()
                .AddSingleton(Options.Create(AppSettings))
                .Configure<HostOptions>(context.Configuration.GetSection(nameof(HostOptions)));

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

            #region "Deprecated version"

            //services.AddSingleton<IDatabaseConfigurator, DatabaseConfigurator>();

            //if (AppSettings.UseProducer)
            //{
            //    ConfigureProducerSettings(services);
            //    services
            //        .AddSingleton<IMessageProducer, TopicMessageProducer>()
            //        .AddSingleton<IDatabaseMessageConsumer, DatabaseMessageConsumer>()
            //        .AddHostedService<MessageProducerService>();
            //}

            //if (AppSettings.UseConsumer)
            //{
            //    ConfigureConsumerSettings(services);
            //    services
            //        .AddSingleton<IMessageConsumer, MessageConsumer>()
            //        .AddSingleton<IDatabaseMessageProducer, DatabaseMessageProducer>()
            //        .AddHostedService<MessageConsumerService>();
            //}

            #endregion
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