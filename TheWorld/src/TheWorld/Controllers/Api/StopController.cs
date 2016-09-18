using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route("/api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<StopController> _logger;
        private GeoCoordsService _coordsService;

        public StopController(IWorldRepository repository, ILogger<StopController> logger, 
            GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName, User.Identity.Name);
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

                var result = await _coordsService.GeoCoordsAsync(newStop.City);

                if (!result.Success)
                {
                    _logger.LogError(result.Message);
                }
                else
                {
                    newStop.Latitude = result.Latitude;
                    newStop.Longitude = result.Longitude;

                    _repository.AddStop(newStop, User.Identity.Name, tripName);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"/api/trips/{tripName}/stops/{newStop.City}",
                            Mapper.Map<StopViewModel>(newStop));
                    }
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
