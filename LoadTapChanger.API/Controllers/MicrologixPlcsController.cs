using AutoMapper;
using LoadTapChanger.API.Static;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlcTagLibrary.Data;
using PlcTagLibrary.Dtos.MicrologixPLC;
using PlcTagLibrary.Models;
using PlcTagLibrary.Repositories;

namespace LoadTapChanger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MicrologixPlcsController : ControllerBase
    {
        private readonly ILogger<MicrologixPlcsController> _logger;
        private readonly LoadTapChangerDBContext _context;
        private readonly IMapper _mapper;
        private readonly IMicrologixPlcRepository _plcRepository;

        public MicrologixPlcsController(ILogger<MicrologixPlcsController> logger, LoadTapChangerDBContext context, IMapper mapper, IMicrologixPlcRepository plcRepository)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _plcRepository = plcRepository;
        }

        // GET: api/MicrologixPlcs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MicrologixPlc>>> GetPlcAll()
        {
            var plc = await _plcRepository.GetAllAsync();
            var plcDto = _mapper.Map<IEnumerable<ReadPlcDto>>(plc);

            return Ok(plcDto);
        }

        // GET: api/MicrologixPlcs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MicrologixPlc>> GetPlc(int id)
        {

            try
            {
                var plc = await _plcRepository.GetAsync(id);
                var plcDto = _mapper.Map<ReadPlcDto>(plc);
                if (plc == null)
                {
                    return NotFound();
                }
                return Ok(plcDto);
            }
            catch (Exception)
            {
                throw;
            }


        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<MicrologixPlc>> GetPlcDetails(int id)
        {
            try
            {
                var plc = await _plcRepository.GetPlcDetailsAsync(id);
                if (plc == null)
                {

                    _logger.LogWarning(message: $"Record Not Found");
                    return NotFound();
                }
                return Ok(plc);
            }
            catch (Exception ex)
            {
                var func = nameof(GetPlcDetails);

                _logger.LogError(message: "Error: {func} - ID: {id} - {ex.Message}", func, id, ex.Message);
                return StatusCode(500, ErrorMessages.Error500);
            }

        }

        // PUT: api/MicrologixPlcs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlc(int id, MicrologixPlc micrologixPlc)
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
                if (!await PlcExists(id))
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
        public async Task<ActionResult<MicrologixPlc>> AddPlc(MicrologixPlc micrologixPlc)
        {
            _context.MicrologixPlcs.Add(micrologixPlc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMicrologixPlc", new { id = micrologixPlc.PlcId }, micrologixPlc);
        }

        // DELETE: api/MicrologixPlcs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlc(int id)
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

        private async Task<bool> PlcExists(int id)
        {
            return await _plcRepository.Exists(id);
        }
    }
}
