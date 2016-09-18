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
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<TripsController> _logger;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAllTrips();

                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get All Trips: {ex.Message}");
                return BadRequest("Error occurred");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(theTrip);
                _repository.AddTrip(newTrip);

                if (await _repository.SaveChangesAsync())
                {
                    //return Created($"api/trips/{theTrip.Name}", theTrip);
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
                return BadRequest("Failed to save changes to the database");
            }
            //return BadRequest("Bad Data");
            return BadRequest(ModelState);
        }

        [HttpGet("/tripsTest")]
        public IActionResult GetTripTest(bool test)
        {
            if (test)
            {
                return Ok(new Trip()
                {
                    Name = "My Trip My Adventure"
                });
            }
            return BadRequest("Bad things happend");
        }

        [HttpGet("/tripsJson")]
        public JsonResult GetTripsJson()
        {
            return Json(new Trip()
            {
                Name = "My Trip My Adventure"
            });
        }
    }
}
