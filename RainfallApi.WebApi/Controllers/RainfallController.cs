using Microsoft.AspNetCore.Mvc;
using RainfallApi.Application.Interface;
using RainfallApi.Domain.Model.Dto;
using System.ComponentModel.DataAnnotations;

namespace RainfallApi.WebApi.Controllers
{
    /// <summary>
    /// Controller for retrieving rainfall readings.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RainfallController : ControllerBase
    {
        private readonly IRainfallReadingService _rainfallService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RainfallController"/> class.
        /// </summary>
        /// <param name="rainfallService">The rainfall service.</param>
        public RainfallController(IRainfallReadingService rainfallService)
        {
            _rainfallService = rainfallService ?? throw new ArgumentNullException(nameof(rainfallService));
        }

        /// <summary>
        /// Retrieves the latest rainfall readings for the specified stationId.
        /// </summary>
        /// <param name="stationId">The id of the reading station.</param>
        /// <param name="count">The number of readings to return (default is 10).</param>
        /// <returns>A list of rainfall readings.</returns>
        /// <response code="200">Returns a list of rainfall readings.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">No readings found for the specified stationId.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("readings")]
        [ProducesResponseType(typeof(RainfallReadingResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> GetRainfallReadings(
            [FromQuery][Required(ErrorMessage = "StationId is required")] string stationId,
            [FromQuery][Range(1, 100, ErrorMessage = "Count must be between 1 and 100")] int count = 10)
        {
            try
            {

                //We can implement our own validation and throw proper response, but why not just use the DataAnnotations `Required` and `Range`?
                //List<ErrorDetail> errors = new();
                //if(string.IsNullOrEmpty(stationId))
                //{
                //    errors.Add(new()
                //    {
                //        PropertyName = nameof(stationId),
                //        Message = "StationId is required"
                //    });
                //}
                //if(count < 1 || count > 100)
                //{
                //    errors.Add(new()
                //    {
                //        PropertyName = nameof(count),
                //        Message = "Count must be between 1 and 100"
                //    });
                //}
                //if (errors.Any()) 
                //{
                //    return BadRequest(new ErrorResponse()
                //    {
                //        Message = "Invalid request",
                //        Details = errors
                //    });
                //}

                var readings = await _rainfallService.GetRainfallReadings(stationId, count);
                if(readings?.Any() == false)
                {
                    return NotFound(new ErrorResponse()
                    {
                        Message = "No readings found for the specified stationId"
                    });
                }

                return Ok(new RainfallReadingResponse(readings));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Message = "Internal server error",
                    Details = new()
                    {
                        new() {
                            PropertyName = ex.Source,
                            Message = ex.ToString()
                        }
                    }
                });
            }
        }
    }
}
