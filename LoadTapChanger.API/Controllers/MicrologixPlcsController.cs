﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlcTagLibrary.Data;
using PlcTagLibrary.Models;
using PlcTagLibrary.Repositories;

namespace LoadTapChanger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MicrologixPlcsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MicrologixPlcsController> _logger;
        private readonly LoadTapChangerDBContext _context;
        private readonly IMicrologixPlcRepository _plcRepository;

        public MicrologixPlcsController(ILogger<MicrologixPlcsController> logger, IMapper mapper, LoadTapChangerDBContext context, IMicrologixPlcRepository plcRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _plcRepository = plcRepository;
        }

        // GET: api/MicrologixPlcs
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<MicrologixPlc>>>> GetPlcs()
        {
            return Ok(await _plcRepository.GetAllPlcAsync());
        }

        // GET: api/MicrologixPlcs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<MicrologixPlc>>> GetPlc(int id)
        {
            return Ok(await _plcRepository.GetPlcByIdAsync(id));

            // try
            // {
            //     var plc = await _plcRepository.GetAsync(id);
            //     var plcDto = _mapper.Map<ReadPlcDto>(plc);
            //     return plc == null ? (ActionResult<MicrologixPlc>)NotFound() : (ActionResult<MicrologixPlc>)Ok(plcDto);
            // }
            // catch (Exception)
            // {
            //     throw;
            // }


        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<MicrologixPlc>> GetPlcDetails(int id)
        {
            return Ok(await _plcRepository.GetPlcDetailsAsync(id));
            // try
            // {
            //     var plc = await _plcRepository.GetPlcDetailsAsync(id);
            //     if (plc == null)
            //     {
            //         _logger.LogWarning(message: $"Record Not Found");
            //         return NotFound();
            //     }
            //     return Ok(plc);
            // }
            // catch (Exception ex)
            // {
            //     var func = nameof(GetPlcDetails);

            //     _logger.LogError(message: "Error: {func} - ID: {id} - {ex.Message}", func, id, ex.Message);
            //     return StatusCode(500, ErrorMessages.Error500);
            // }

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
                _ = await _context.SaveChangesAsync();
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
            _ = _context.MicrologixPlcs.Add(micrologixPlc);
            _ = await _context.SaveChangesAsync();

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

            _ = _context.MicrologixPlcs.Remove(micrologixPlc);
            _ = await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> PlcExists(int id)
        {
            return await _plcRepository.Exists(id);
        }
    }
}
