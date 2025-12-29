using AirQuality.Application.DTO;
using AirQuality.Application.Mapper.DTOMapper;
using AirQuilty.Domain.Entitiy;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirQuality.Application.Mapper
{
    public class AirQualityMappingProfile :Profile
    {
        public AirQualityMappingProfile()
        {
            CreateMap<AirQualitySnapshot, AirQualityResponseDto>();
            CreateMap<AirQualityResponseDto, AirQualitySnapshot>();
        }
    }
}
