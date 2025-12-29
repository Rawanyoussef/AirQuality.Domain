using AirQuality.Application.DTO;
using AirQuilty.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Application.Interfaces
{
    public interface IAirQualityService
    {
        Task<AirQualityResponseDto> GetNearestCityAirQualityAsync(double lat, double lon);
        Task<AirQualityResponseDto> GetMostPollutedParisAsync();
        Task AddSnapshotAsync(AirQualitySnapshot snapshot);


    }
}
