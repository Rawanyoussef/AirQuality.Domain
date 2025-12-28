using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuilty.Domain.Entitiy
{
    public class AirQualitySnapshot
    {
        public int Id { get; set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string State { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public int AqiUS { get; private set; }
        public string MainPollutant { get; set; }
        public DateTime FetchedAtUtc { get;  set; }
        public string RawJson { get;  set; }
        public static AirQualitySnapshot Create(
                 string city,
                 string state,
                 string country,
                 int aqiUS,
                 string mainPollutant,
                 DateTime timestamp)
        {
            return new AirQualitySnapshot
            {
                City = city,
                State = state,
                Country = country,
                AqiUS = aqiUS,
                MainPollutant = mainPollutant,
                FetchedAtUtc = timestamp
            };
        }
    }


}

