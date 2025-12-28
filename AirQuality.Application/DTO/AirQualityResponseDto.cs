using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Application.DTO
{
    public class AirQualityResponseDto
    {

        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int AqiUS { get; set; }         
        public string MainPollutant { get; set; }  
        public DateTime Timestamp { get; set; }
    }
}
