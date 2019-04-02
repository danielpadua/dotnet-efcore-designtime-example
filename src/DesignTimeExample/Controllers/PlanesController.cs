using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PlanesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PlanesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/planes        
        /// <summary>
        /// Get all planes at once
        /// </summary>
        /// <returns>All planes</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Plane>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPlanesAsync()
        {
            return Ok(await _dbContext.Planes
                                .Include(x => x.Flights)
                                .ToListAsync());
        }

        // GET api/planes/5
        /// <summary>
        /// Get a single plane by id
        /// </summary>
        /// <param name="id">Plane's id</param>
        /// <returns>A plane model</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<Plane>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlaneByIdAsync(int id)
        {
            var plane = await _dbContext.Planes
                                .Include(x => x.Flights)
                                .SingleOrDefaultAsync(x => x.Id == id);
            if (plane == null)
            {
                return NotFound();
            }
            return Ok(plane);
        }

        // POST planes/values
        /// <summary>
        /// Create a plane
        /// </summary>
        /// <param name="plane">A plane model to create</param>
        /// <returns>Created plane model with database generated id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Plane), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostPlaneAsync([FromBody] Plane plane)
        {
            _dbContext.Planes.Add(plane);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPlaneByIdAsync), new { id = plane.Id }, plane);
        }

        // PUT api/planes/5
        /// <summary>
        /// Update a plane        
        /// </summary>
        /// <param name="id">Plane's id</param>
        /// <param name="plane">A plane model to update</param>
        /// <returns>Updated plane model</returns>        
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Plane), StatusCodes.Status200OK)]
        public async Task<IActionResult> PutPlaneAsync(int id, [FromBody] Plane plane)
        {
            var planeDb = await _dbContext.Planes
                                    .Include(x => x.Flights)
                                    .SingleOrDefaultAsync(x => x.Id == id);
            if (planeDb == null)
            {
                return NotFound();
            }
            planeDb.SeatsNumber = plane.SeatsNumber;
            planeDb.Model = plane.Model;
            planeDb.Flights = plane.Flights;
            await _dbContext.SaveChangesAsync();
            return Ok(planeDb);
        }

        // DELETE api/planes/5
        /// <summary>
        /// Delete a plane
        /// Warning: This will not delete associated flights
        /// </summary>
        /// <param name="id">Plane's id</param>
        /// <returns>Http 200 ok if plane was deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePlaneAsync(int id)
        {
            var plane = await _dbContext.Planes.SingleOrDefaultAsync(x => x.Id == id);
            if (plane == null)
            {
                return NotFound();
            }
            _dbContext.Planes.Remove(plane);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
