using AirQuality.Application.Interfaces;
using AirQuilty.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Application.Services
{
    public class AirQualityService
    {
        private readonly IQAir _iqAirClient;
        private readonly IAirQualitySnapshotFactory _factory;
        public AirQualityService(IQAir iqAirClient ,IAirQualitySnapshotFactory factory)
        {
            _iqAirClient = iqAirClient;
            _factory = factory;
        }

        public async Task<AirQualitySnapshot> GetNearestCityAirQualityAsync(double lat, double lon)
        {
            var dto = await _iqAirClient.GetAirQualityAsync(lat, lon);
            var snapshot = _factory.CreateFromApiResponse(dto);
            return snapshot;
        }
    }
}
