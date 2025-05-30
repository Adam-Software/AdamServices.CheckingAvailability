﻿using CheckingAvailability.Interface;
using CheckingAvailability.Services;
using DefaultArguments.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using ServiceFileCreator.Extensions;
using ServiceFileCreator.Model;
using System;
using System.Threading.Tasks;

namespace CheckingAvailability
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {     
                    config.Sources.Clear();
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                
                .ConfigureServices((context, services) =>
                {
                    services.AddAdamArgumentsParserTransient<ArgumentService>(args);

                    SettingsService options = new();
                    context.Configuration.GetRequiredSection("AppSettingsOptions").Bind(options);
                    services.AddSingleton<ISettingsService>(options);
                    
                    services.AddLogging(loggingBuilder =>
                    {
                        Logger logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(context.Configuration)
                            .CreateLogger();
                        
                        loggingBuilder.ClearProviders();
                        loggingBuilder.AddSerilog(logger, dispose: true);
                    });
                    
                    services.AddAdamServiceFileCreator();
                    services.AddHostedService<CheckingAvailabilityService>();
                })
                .Build();

            host.UseAdamServiceFileCreator(projectType: ProjectType.DotnetProject);
            await host.ParseAndRunAsync();
        }
    }
}
