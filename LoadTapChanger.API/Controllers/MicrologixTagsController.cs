using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlcTagLibrary.Data;
using PlcTagLibrary.Models;

namespace LoadTapChanger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MicrologixTagsController : ControllerBase
    {
        private readonly LoadTapChangerDBContext _context;

        public MicrologixTagsController(LoadTapChangerDBContext context)
        {
            _context = context;
        }

        // GET: api/MicrologixTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlcTag>>> GetMicrologixTags()
        {
            return await _context.PlcTags.ToListAsync();
        }

        // GET: api/MicrologixTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlcTag>> GetMicrologixTag(int id)
        {
            var micrologixTag = await _context.PlcTags.FindAsync(id);

            if (micrologixTag == null)
            {
                return NotFound();
            }

            return micrologixTag;
        }

        // PUT: api/MicrologixTags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMicrologixTag(int id, PlcTag micrologixTag)
        {
            if (id != micrologixTag.TagId)
            {
                return BadRequest();
            }

            _context.Entry(micrologixTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MicrologixTagExists(id))
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

        // POST: api/MicrologixTags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlcTag>> PostMicrologixTag(PlcTag micrologixTag)
        {
            _context.PlcTags.Add(micrologixTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMicrologixTag", new { id = micrologixTag.TagId }, micrologixTag);
        }

        // DELETE: api/MicrologixTags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMicrologixTag(int id)
        {
            var micrologixTag = await _context.PlcTags.FindAsync(id);
            if (micrologixTag == null)
            {
                return NotFound();
            }

            _context.PlcTags.Remove(micrologixTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MicrologixTagExists(int id)
        {
            return _context.PlcTags.Any(e => e.TagId == id);
        }
    }
}
