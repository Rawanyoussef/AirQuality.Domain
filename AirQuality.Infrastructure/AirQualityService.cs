using AirQuality.Application.DTO;
using AirQuality.Application.Interfaces;
using AirQuilty.Domain.Entitiy;
using AutoMapper;

namespace AirQuality.Application.Services
{
    public class AirQualityService : IAirQualityService
    {
        private readonly IQAir _iqAirClient;
        private readonly IAirQualitySnapshotFactory _factory;
        private readonly IAirQualitySnapshotRepository _repository;
        private readonly IMapper _mapper;


        public AirQualityService(
            IQAir iqAirClient,
            IAirQualitySnapshotFactory factory,
            IAirQualitySnapshotRepository repository ,IMapper mapper)
        {
            _iqAirClient = iqAirClient;
            _factory = factory;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AirQualityResponseDto> GetNearestCityAirQualityAsync(double lat, double lon)
        {
            var dto = await _iqAirClient.GetAirQualityAsync(lat, lon);
            var snapshot = _factory.CreateFromApiResponse(dto);

            await _repository.AddAsync(snapshot);

            return _mapper.Map<AirQualityResponseDto>(snapshot);
        }

        


        public async Task<AirQualityResponseDto> GetMostPollutedParisAsync()
        {
            var snapshot = await _repository.GetMostPollutedParisAsync();
            return _mapper.Map<AirQualityResponseDto>(snapshot);
        }


        public async Task AddSnapshotAsync(AirQualitySnapshot snapshot)
        {
            await _repository.AddAsync(snapshot);
        }

    }
}
