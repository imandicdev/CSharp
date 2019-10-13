using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pools.Models;

namespace Pools.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly PoolsContext _context;

        public ReservationsController(PoolsContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public IEnumerable<Reservations> GetReservations()
        {
            return _context.Reservations;
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservations([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservations = await _context.Reservations.FindAsync(id);

            if (reservations == null)
            {
                return NotFound();
            }

            return Ok(reservations);
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservations([FromRoute] int id,[FromBody] Reservations reservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservations.ID)
            {
                return BadRequest();
            }

            _context.Entry(reservations).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reservations
        [Route("/v1/pools/sessions/{id}/reserve")]
        [HttpPost]
    
        public async Task<IActionResult> PostReservations([FromBody] Reservations reservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Reservations.Add(reservations);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservations", new { id = reservations.ID }, reservations);
 
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservations([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservations = await _context.Reservations.FindAsync(id);
            if (reservations == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservations);
            await _context.SaveChangesAsync();

            return Ok(reservations);
        }

        private bool ReservationsExists(int id)
        {
            return _context.Reservations.Any(e => e.ID == id);
        }
    }
}