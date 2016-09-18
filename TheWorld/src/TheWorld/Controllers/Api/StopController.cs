using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("/api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<StopController> _logger;

        public StopController(IWorldRepository repository, ILogger<StopController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName);
                var results = Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(x => x.Order).ToList());
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get stops: {0}", ex);
            }
            return BadRequest("Failed to get stops");
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]StopViewModel stopViewModel, string tripName)
        {
            try
            {
                var newStop = Mapper.Map<Stop>(stopViewModel);

                _repository.AddStop(newStop, tripName);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/trips/{tripName}/stops/{newStop.City}",
                        Mapper.Map<StopViewModel>(newStop));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new stops: {0}", ex);
            }
            return BadRequest("Failed to save new stops");
        }
    }
}
