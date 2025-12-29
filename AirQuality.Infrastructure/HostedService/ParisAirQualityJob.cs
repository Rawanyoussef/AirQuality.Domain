using AirQuality.Application.DTO;
using AirQuality.Application.Interfaces;
using AirQuilty.Domain.Entitiy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class ParisAirQualityJob : IHostedService, IDisposable
{
    private readonly ILogger<ParisAirQualityJob> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer _timer;

    public ParisAirQualityJob(ILogger<ParisAirQualityJob> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Paris Air Quality Job started.");
        _timer = new Timer(async _ => { await DoWork(); }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        return Task.CompletedTask;
    }

    private async Task DoWork()
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var airQualityService = scope.ServiceProvider.GetRequiredService<IAirQualityService>();
            double lat = 48.856613;
            double lon = 2.352222;
            AirQualityResponseDto dto = await airQualityService.GetNearestCityAirQualityAsync(lat, lon);

            if (dto == null)
            {
                _logger.LogWarning("API returned null DTO, skipping save.");
                return;
            }

            if (string.IsNullOrEmpty(dto.Rawjson))
            {
                _logger.LogWarning("RawJson is null or empty, setting default value.");
                dto.Rawjson = "{}";
            }

            var snapshot = AirQualitySnapshot.Create(
                city: dto.City ?? "Unknown",
                state: dto.State ?? "Unknown",
                country: dto.Country ?? "Unknown",
                aqiUS: dto.AqiUS,
                mainPollutant: dto.MainPollutant ?? "Unknown",
                timestamp: dto.Timestamp,
                rawjson: dto.Rawjson
            );

            await airQualityService.AddSnapshotAsync(snapshot);
            _logger.LogInformation($"Paris Air Quality fetched and saved at {DateTime.UtcNow}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching Paris air quality.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        _logger.LogInformation("Paris Air Quality Job stopped.");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
