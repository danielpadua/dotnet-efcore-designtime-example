using System.Collections.Generic;
using System.Threading.Tasks;
using DesignTimeExample.Data;
using DesignTimeExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesignTimeExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public FlightsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/flights        
        /// <summary>
        /// Get all flights at once
        /// </summary>
        /// <returns>All flights</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Flight>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFlightsAsync()
        {
            return Ok(await _dbContext.Flights
                                .Include(x => x.Plane)
                                .ToListAsync());
        }

        // GET api/flights/5
        /// <summary>
        /// Get a single flight by id
        /// </summary>
        /// <param name="id">Flight's id</param>
        /// <returns>A flight model</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<Flight>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFlightByIdAsync(int id)
        {
            var flight = await _dbContext.Flights
                                .Include(x => x.Plane)
                                .SingleOrDefaultAsync(x => x.Id == id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        // POST flights/values
        /// <summary>
        /// Create a flight
        /// </summary>
        /// <param name="flight">A flight model to create</param>
        /// <returns>Created flight model with database generated id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Flight), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostFlightAsync([FromBody] Flight flight)
        {
            _dbContext.Flights.Add(flight);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFlightByIdAsync), new { id = flight.Id }, flight);
        }

        // PUT api/flights/5
        /// <summary>
        /// Update a flight        
        /// </summary>
        /// <param name="id">Flight's id</param>
        /// <param name="flight">A flight model to update</param>
        /// <returns>Updated flight model</returns>        
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Flight), StatusCodes.Status200OK)]
        public async Task<IActionResult> PutFlightAsync(int id, [FromBody] Flight flight)
        {
            var flightDb = await _dbContext.Flights
                                    .Include(x => x.Plane)
                                    .SingleOrDefaultAsync(x => x.Id == id);
            if (flightDb == null)
            {
                return NotFound();
            }
            // flightDb.Code = flight.Code; Code is not updatable
            flightDb.PassengerNumber = flight.PassengerNumber;
            flightDb.Plane = flight.Plane;
            await _dbContext.SaveChangesAsync();
            return Ok(flightDb);
        }

        // DELETE api/flights/5
        /// <summary>
        /// Delete a flight
        /// Warning: This will not delete associated planes
        /// </summary>
        /// <param name="id">Flight's id</param>
        /// <returns>Http 200 ok if flight was deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFlightAsync(int id)
        {
            var flight = await _dbContext.Flights.SingleOrDefaultAsync(x => x.Id == id);
            if (flight == null)
            {
                return NotFound();
            }
            _dbContext.Flights.Remove(flight);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}