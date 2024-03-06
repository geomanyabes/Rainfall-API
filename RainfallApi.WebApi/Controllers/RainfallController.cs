﻿using Microsoft.AspNetCore.Mvc;
using RainfallApi.Domain.Model.Entity;

namespace RainfallApi.WebApi.Controllers
{
    public class RainfallController
    {
        [ApiController]
        [Route("[controller]")]
        public class RainfallController : ControllerBase
        {
            private readonly RainfallService _rainfallService;

            public RainfallController(RainfallService rainfallService)
            {
                _rainfallService = rainfallService ?? throw new ArgumentNullException(nameof(rainfallService));
            }

            [HttpGet("{stationId}/readings")]
            public async Task<ActionResult<IEnumerable<RainfallReading>>> GetRainfallReadings(
                [FromRoute] string stationId,
                [FromQuery(Name = "count")] int count = 10)
            {
                try
                {
                    // Call the service layer to get rainfall readings
                    var rainfallReadings = await _rainfallService.GetRainfallReadings(stationId, count);

                    // Check if any readings were found
                    if (rainfallReadings == null || rainfallReadings.Count == 0)
                    {
                        // Return 404 Not Found if no readings were found
                        return NotFound("No readings found for the specified stationId");
                    }

                    // Return the list of rainfall readings
                    return Ok(rainfallReadings);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    // Return 500 Internal Server Error
                    return StatusCode(500, "Internal server error");
                }
            }
        }
    }
}