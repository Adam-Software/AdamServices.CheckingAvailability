using CheckingAvailability.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace CheckingAvailability.Services
{
    public class CheckingAvailabilityService : BackgroundService
    {
        #region Services

        private readonly ILogger<CheckingAvailabilityService> mLogger;
        private readonly IHostApplicationLifetime mHostApplicationLifetime;

        #endregion

        #region Var

        private readonly int[] mPorts = [15000, 16000];

        #endregion

        #region ~

        public CheckingAvailabilityService(IServiceProvider serviceProvider)
        {
            mLogger = serviceProvider.GetRequiredService<ILogger<CheckingAvailabilityService>>();
            mHostApplicationLifetime = serviceProvider.GetRequiredService<IHostApplicationLifetime>();

            ArgumentService argumentService = serviceProvider.GetService<ArgumentService>();
            ISettingsService settingsService = serviceProvider.GetRequiredService<ISettingsService>();

            int oldPort = settingsService.OldPortCheckingAvailability;
            int newPort = settingsService.NewPortCheckingAvailability;

            if (argumentService.OldPortCheckingAvailability != 0)
                oldPort = argumentService.OldPortCheckingAvailability;

            if (argumentService.NewPortCheckingAvailability != 0)
                newPort = argumentService.NewPortCheckingAvailability;

            mPorts = [oldPort, newPort];

            mLogger.LogInformation("Service runnig with listens settings on old port {oldPort}", oldPort);
            mLogger.LogInformation("Service runnig with listens settings on new port {newPort}", newPort);
        }

        #endregion

        private void AcceptTcpClientAsync(TcpListener listener, int port, CancellationToken stoppingToken)
        {
            Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        _ = await listener.AcceptTcpClientAsync(stoppingToken);
                        mLogger.LogInformation("Client connected to {port}", port);
                    }
                    catch (OperationCanceledException)
                    {                    
                        while (listener != null)
                        {
                            mLogger.LogInformation("Server on {port} was canceled", port);
                            listener.Stop();
                            listener.Dispose();
                            listener = null;
                        }
                    }
                }
            }, stoppingToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int portsCounter = mPorts.Length;

            foreach (int port in mPorts)
            {
                try
                {
                    TcpListener listener = new(IPAddress.Any, port);
                    listener.Start();
                    mLogger.LogInformation("Server running on {port}", port);

                    AcceptTcpClientAsync(listener, port, stoppingToken);
                    
                }
                catch (SocketException)
                {
                    mLogger.LogError("Port {port} is busy. The server is not running.", port);
        
                    portsCounter--;

                    if (portsCounter == 0)
                        mHostApplicationLifetime.StopApplication();
                }
                catch (Exception ex)
                {
                    mLogger.LogError("{error}", ex);
                }
            }

            return Task.CompletedTask;
        }
    }
}
