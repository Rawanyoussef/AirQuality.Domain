using AirQuality.Application.DTO;
using AirQuality.Application.Interfaces;
using AirQuilty.Domain.Entitiy;

namespace AirQuality.Application.Services
{
    public class AirQualityService : IAirQualityService
    {
        private readonly IQAir _iqAirClient;
        private readonly IAirQualitySnapshotFactory _factory;
        private readonly IAirQualitySnapshotRepository _repository;

        public AirQualityService(
            IQAir iqAirClient,
            IAirQualitySnapshotFactory factory,
            IAirQualitySnapshotRepository repository)
        {
            _iqAirClient = iqAirClient;
            _factory = factory;
            _repository = repository;
        }

        public async Task<AirQualitySnapshot> GetNearestCityAirQualityAsync(double lat, double lon)
        {
            var dto = await _iqAirClient.GetAirQualityAsync(lat, lon);
            var snapshot = _factory.CreateFromApiResponse(dto);
            return snapshot;
        }


        public async Task<AirQualitySnapshot> GetMostPollutedParisAsync()
        {
            return await _repository.GetMostPollutedParisAsync();
        }


        public async Task AddSnapshotAsync(AirQualitySnapshot snapshot)
        {
            await _repository.AddAsync(snapshot);
        }

    }
}
