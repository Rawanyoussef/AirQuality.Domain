using AirQuality.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirQualityController : ControllerBase
    {
        private readonly IAirQualityService _service;

        public AirQualityController(IAirQualityService service)
        {
            _service = service;
        }


        [HttpGet("nearest-city")]
        public async Task<IActionResult> GetNearestCity([FromQuery] double lat, [FromQuery] double lon)
        {
            if (lat < -90 || lat > 90 || lon < -180 || lon > 180)
                return BadRequest("Invalid coordinates.");

            var snapshot = await _service.GetNearestCityAirQualityAsync(lat, lon);
            return Ok(snapshot);
        }

        [HttpGet("paris/most-polluted")]
        public async Task<IActionResult> GetMostPollutedParis()
        {
            var snapshot = await _service.GetMostPollutedParisAsync();
            if (snapshot == null)
                return NotFound("No data for Paris found.");
            return Ok(snapshot);
        }
    }
}
