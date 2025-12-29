using AirQuality.Application.DTO;
using AirQuality.Application.Interfaces;
using AirQuality.Application.Validators;
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
        public async Task<IActionResult> GetNearestCity(double lat, double lon)
        {
            var validator = new NearestCityRequestValidator();
            var result = validator.Validate((lat, lon));

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            try
            {
                var snapshot = await _service.GetNearestCityAirQualityAsync(lat, lon);
                return Ok(snapshot);
            }
            catch (ApplicationException ex) 
            {
                return StatusCode(502, ex.Message);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("paris/most-polluted")]
        public async Task<IActionResult> GetMostPollutedParis()
        {
            try
            {
                var dto = await _service.GetMostPollutedParisAsync();
                if (dto == null)
                    return NotFound("No data for Paris found.");
                return Ok(dto);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(502, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
