using AirQuality.Application.DTO;
using AirQuality.Application.Interfaces;
using AirQuilty.Domain.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Application.Services
{
    public class AirQualitySnapshotFactory : IAirQualitySnapshotFactory
    {
        public AirQualitySnapshot CreateFromApiResponse(AirQualityResponseDto response)
        {
          return AirQualitySnapshot.Create
        (
            city: response.City,
            state: response.State,
            country: response.Country,
            aqiUS: response.AqiUS,
            mainPollutant: response.MainPollutant,
            timestamp: response.Timestamp,
            rawjson: response.Rawjson

        );
        }

    }
}


