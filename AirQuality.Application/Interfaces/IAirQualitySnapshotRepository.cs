using AirQuilty.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Application.Interfaces
{
    public interface IAirQualitySnapshotRepository
    {
        Task AddAsync(AirQualitySnapshot airQuality);
        Task<AirQualitySnapshot> GetMostPollutedParisAsync();

    }
}

