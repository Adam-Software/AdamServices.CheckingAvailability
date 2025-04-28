using CheckingAvailability.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckingAvailability.Services
{
    public class CheckingAvailabilityService : BackgroundService
    {
        #region Services

        private readonly ILogger<CheckingAvailabilityService> mLogger;

        #endregion

        #region ~

        public CheckingAvailabilityService(IServiceProvider serviceProvider)
        {
            mLogger = serviceProvider.GetRequiredService<ILogger<CheckingAvailabilityService>>();

            ArgumentService argumentService = serviceProvider.GetService<ArgumentService>();
            ISettingsService settingsService = serviceProvider.GetRequiredService<ISettingsService>();

            int oldPort = settingsService.OldPortCheckingAvailability;
            int newPort = settingsService.NewPortCheckingAvailability;

            if(argumentService.OldPortCheckingAvailability != 0)
                oldPort = argumentService.OldPortCheckingAvailability;

            if(argumentService.NewPortCheckingAvailability != 0)
                newPort = argumentService.NewPortCheckingAvailability;

            mLogger.LogInformation("Service runnig with listens on old port {oldPort}", oldPort);
            mLogger.LogInformation("Service runnig with listens on new port {newPort}", newPort);
        }

        #endregion

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    
                }
            }
            catch (OperationCanceledException)
            {
                
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                
            }
        }
    }
}
