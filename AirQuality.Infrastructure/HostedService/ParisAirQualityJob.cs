using AirQuality.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Infrastructure.HostedService
{
    public class ParisAirQualityJob : IHostedService, IDisposable
    {
        private readonly ILogger<ParisAirQualityJob> _logger;
        private readonly IAirQualityService _airQualityService;
        private Timer _timer;

        public ParisAirQualityJob(ILogger<ParisAirQualityJob> logger, IAirQualityService airQualityService)
        {
            {
                _logger = logger;
                _airQualityService = airQualityService;
            }
        }
      public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Paris Air Quality Job started.");

            _timer = new Timer(async _ =>
            {
                await DoWork();
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private async Task DoWork()
        {
            try
            {
                double lat = 48.856613;
                double lon = 2.352222;

                var snapshot = await _airQualityService.GetNearestCityAirQualityAsync(lat, lon);

                await _airQualityService.AddSnapshotAsync(snapshot);

                _logger.LogInformation($"Paris Air Quality fetched and saved at {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Paris air quality.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Paris Air Quality Job stopped.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }


}

    
