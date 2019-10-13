using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pools.Models;

namespace Pools.Controllers
{   [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        
        
        private readonly PoolsContext _context;

        public SessionsController(PoolsContext context)
        {
            _context = context;

            if (_context.Sessions.Count() == 0)
            {
                _context.Sessions.Add(new Models.Sessions { ID = 1, From = "08:00", To = "08:30", Name = "Jutarnji" });
                _context.Sessions.Add(new Models.Sessions { ID = 2, From = "12:00", To = "13:00", Name = "Popodnevni" });
                _context.Sessions.Add(new Models.Sessions { ID = 3, From = "19:30", To = "21:30", Name = "Večernji" });
                _context.SaveChanges();
            }
        }

        // GET: api/Sessions
        [HttpGet]
        public IEnumerable<Sessions> GetSessions()
        {
            return _context.Sessions;
        }

        // GET: api/Sessions/5

        [HttpGet("/v1/pools/{id}/[controller]")]
        public async Task<IActionResult> GetSessions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sessions = await _context.Sessions.FindAsync(id);

            if (sessions == null)
            {
                return NotFound();
            }

            return Ok(sessions);
        }

        // PUT: api/Sessions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSessions([FromRoute] int id, [FromBody] Sessions sessions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sessions.ID)
            {
                return BadRequest();
            }

            _context.Entry(sessions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionsExists(id))
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

        // POST: api/Sessions
        [HttpPost]
        public async Task<IActionResult> PostSessions([FromBody] Sessions sessions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sessions.Add(sessions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSessions", new { id = sessions.ID }, sessions);
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sessions = await _context.Sessions.FindAsync(id);
            if (sessions == null)
            {
                return NotFound();
            }

            _context.Sessions.Remove(sessions);
            await _context.SaveChangesAsync();

            return Ok(sessions);
        }

        private bool SessionsExists(int id)
        {
            return _context.Sessions.Any(e => e.ID == id);
        }
    }
}