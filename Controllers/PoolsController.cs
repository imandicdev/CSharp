using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pools.Models;

namespace Pools.Controllers
{   [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class PoolsController : ControllerBase
    {
        private readonly ILogger<PoolsController> _logger;

        
        private readonly PoolsContext _context;

        public PoolsController(PoolsContext context, ILogger<PoolsController> logger)
        {
            _context = context;
            _logger = logger;

            if (_context.Pools.Count() == 0)
            {
                _context.Pools.Add(new Models.Pools { ID = 1, Name = "Olimpijski", Size = "50x25m", Deepth = "2m" });
                _context.Pools.Add(new Models.Pools { ID = 2, Name = "Rekreativni", Size = "25x16m", Deepth = "1.20m" });
                _context.Pools.Add(new Models.Pools { ID = 3, Name = "Dečiji", Size = "20x12m", Deepth = "20cm" });
                _context.SaveChanges();
            }
        }

        // GET: v1/Pools
        [HttpGet]
        public IEnumerable<Models.Pools> GetPools()
        {
            //try
            //{
            //    _logger.LogInformation("Testing HttpGet method for Pools controller :(");
            //    
            // }
            //
            // catch (Exception e)
            // {
            //     _logger.LogError(e.Message.ToString());
            // }

            _logger.LogDebug("/v1/Pools was called");
            return _context.Pools;

        }

        // GET: v1/Pools/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPools([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pools = await _context.Pools.FindAsync(id);

            if (pools == null)
            {
                return NotFound();
            }

            return Ok(pools);
        }

       
        // PUT: v1/Pools/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPools([FromRoute] int id, [FromBody] Models.Pools pools)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pools.ID)
            {
                return BadRequest();
            }

            _context.Entry(pools).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoolsExists(id))
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



        // POST: v1/Pools
        
        [HttpPost]
        public async Task<IActionResult> PostPools([FromBody] Models.Pools pools)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pools.Add(pools);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPools", new { id = pools.ID }, pools);
        }

        // DELETE: v1/Pools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePools([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pools = await _context.Pools.FindAsync(id);
            if (pools == null)
            {
                return NotFound();
            }

            _context.Pools.Remove(pools);
            await _context.SaveChangesAsync();

            return Ok(pools);
        }

        private bool PoolsExists(int id)
        {
            return _context.Pools.Any(e => e.ID == id);
        }
    }
}