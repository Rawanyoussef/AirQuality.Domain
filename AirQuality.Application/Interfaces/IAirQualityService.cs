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
        Task<AirQualitySnapshot> GetNearestCityAirQualityAsync(double lat, double lon);
        Task<AirQualitySnapshot> GetMostPollutedParisAsync();
        Task AddSnapshotAsync(AirQualitySnapshot snapshot);


    }
}
