

using CustomWeatherClientTool.API;
using CustomWeatherClientTool.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace CustomWeatherClientTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
           var host = CreateHostBuilder(args).Build();


            var myService = host.Services.GetRequiredService<CustomWeatherClientToolProcessor>();
            myService.Execute();
        }

        public static IHostBuilder CreateHostBuilder(string[]args)
        {
            var builder = Host.CreateDefaultBuilder();

            var configbuilder = new ConfigurationBuilder();
            ConfigureConfiguration(configbuilder, args);
            
            var config=configbuilder.Build();

            builder.ConfigureAppConfiguration(op =>
            {
                op.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                op.AddJsonFile("in.json", optional: true, reloadOnChange: true);
            })
              .ConfigureServices((hostcontext, services) =>
              {
                  services.Configure<Settings>(hostcontext.Configuration.GetSection("Settings"));
                  services.Configure<List<CityList>>(hostcontext.Configuration.GetSection("CityList"));

                  services.AddTransient<CustomWeatherClientToolProcessor>();
                  services.AddTransient<WeatherInfoAPI>();

              });

            return builder;
        }

        public static void ConfigureConfiguration  (IConfigurationBuilder configurationBuilder, string[] args)
        {
            configurationBuilder.AddCommandLine(args);
            configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("in.json", optional: true, reloadOnChange: true);
            configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


        }
    }
}