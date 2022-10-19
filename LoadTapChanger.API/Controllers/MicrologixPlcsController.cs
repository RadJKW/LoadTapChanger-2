using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlcTagLibrary.Datas;
using PlcTagLibrary.Models;

namespace LoadTapChanger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MicrologixPlcsController : ControllerBase
    {
        private readonly LoadTapChangerDBContext _context;

        public MicrologixPlcsController(LoadTapChangerDBContext context)
        {
            _context = context;
        }

        // GET: api/MicrologixPlcs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MicrologixPlc>>> GetMicrologixPlcs()
        {
            return await _context.MicrologixPlcs.ToListAsync();
        }

        // GET: api/MicrologixPlcs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MicrologixPlc>> GetMicrologixPlc(int id)
        {
            var micrologixPlc = await _context.MicrologixPlcs.FindAsync(id);

            if (micrologixPlc == null)
            {
                return NotFound();
            }

            return micrologixPlc;
        }

        // PUT: api/MicrologixPlcs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMicrologixPlc(int id, MicrologixPlc micrologixPlc)
        {
            if (id != micrologixPlc.PlcId)
            {
                return BadRequest();
            }

            _context.Entry(micrologixPlc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MicrologixPlcExists(id))
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

        // POST: api/MicrologixPlcs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MicrologixPlc>> PostMicrologixPlc(MicrologixPlc micrologixPlc)
        {
            _context.MicrologixPlcs.Add(micrologixPlc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMicrologixPlc", new { id = micrologixPlc.PlcId }, micrologixPlc);
        }

        // DELETE: api/MicrologixPlcs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMicrologixPlc(int id)
        {
            var micrologixPlc = await _context.MicrologixPlcs.FindAsync(id);
            if (micrologixPlc == null)
            {
                return NotFound();
            }

            _context.MicrologixPlcs.Remove(micrologixPlc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MicrologixPlcExists(int id)
        {
            return _context.MicrologixPlcs.Any(e => e.PlcId == id);
        }
    }
}
