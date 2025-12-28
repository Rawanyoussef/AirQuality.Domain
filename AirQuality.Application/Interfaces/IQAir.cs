using AirQuality.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Application.Interfaces
{
    public interface IQAir
    {
        Task<AirQualityResponseDto> GetAirQualityAsync(double latitude, double longitude);

    }
}
