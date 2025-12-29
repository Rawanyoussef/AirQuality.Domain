using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Application.Mapper.DTOMapper
{
    public class NearestCityRequest
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
