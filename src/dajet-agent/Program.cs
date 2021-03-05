using DaJet.Agent.Consumer;
using DaJet.Agent.Producer;
using DaJet.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;

namespace DaJet.Agent.Service
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices(ConfigureServices);
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            AppSettings settings = ConfigureAppSettings(services);
            if (settings.UseProducer)
            {
                services.AddSingleton<IMessageProducer, MessageProducer>();
                services.AddSingleton<IDatabaseMessageConsumer, DatabaseMessageConsumer>();
                services.AddHostedService<MessageProducerService>();
            }
            if (settings.UseConsumer)
            {
                services.AddSingleton<IMessageConsumer, MessageConsumer>();
                services.AddSingleton<IDatabaseMessageProducer, DatabaseMessageProducer>();
                services.AddHostedService<MessageConsumerService>();
            }
        }
        private static AppSettings ConfigureAppSettings(IServiceCollection services)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string catalogPath = Path.GetDirectoryName(asm.Location);

            AppSettings settings = new AppSettings();
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(catalogPath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            config.Bind(settings);
            services.Configure<AppSettings>(config);
            services.Configure<HostOptions>(config.GetSection("HostOptions"));

            FileLogger.LogSize = settings.LogSize;
            
            ConfigureProducerSettings(services, catalogPath);
            ConfigureConsumerSettings(services, catalogPath);

            return settings;
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