using EdDSAJwtBearerResources.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
namespace EdDSAJwtBearerResources.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetData")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetAdminData")]
        [Authorize(Policy = RolePolicies.Admin)]
        public IActionResult GetAdminData()
        {
            return Ok("Admin Data");
        }

        [HttpGet("GetAccountantData")]
        [Authorize(Policy = RolePolicies.Accountant)]
        public IActionResult GetAccountantData()
        {
            return Ok("Accountant Data");
        }

        [HttpGet("GetSellerData")]
        [Authorize(Policy = RolePolicies.Seller)]
        public IActionResult GetSellerData()
        {
            return Ok("Seller Data");
        }

    }
}
