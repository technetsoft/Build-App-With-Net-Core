using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repository)
        {
            _repository = repository;
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
                return BadRequest("Error occurred");
            }
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(theTrip);
                //return Created($"api/trips/{theTrip.Name}", theTrip);
                return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
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
