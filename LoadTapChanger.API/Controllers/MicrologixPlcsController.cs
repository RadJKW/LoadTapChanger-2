﻿using AutoMapper;
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
        private readonly IMicrologixPlcRepository _plcRepository;

        public MicrologixPlcsController(ILogger<MicrologixPlcsController> logger, LoadTapChangerDBContext context, IMicrologixPlcRepository plcRepository)
        {
            _logger = logger;

            _context = context;
            _plcRepository = plcRepository;
        }

        // GET: api/MicrologixPlcs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadPlcDto>>> GetPlcs()
        {
            var response = await _plcRepository.GetAllPlcAsync();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }

        // GET: api/MicrologixPlcs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<MicrologixPlc>>> GetPlc(int id)
        {
            return Ok(await _plcRepository.GetPlcByIdAsync(id));
        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<ServiceResponse<MicrologixPlc>>> GetPlcDetails(int id)
        {
            return Ok(await _plcRepository.GetPlcDetailsAsync(id));
        }

        // PUT: api/MicrologixPlcs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<UpdatePlcDto>>> UpdatePlc(int id, UpdatePlcDto updatePlcDto)
        {
            var response = await _plcRepository.UpdatePlcAsync(updatePlcDto);
            if (response.Success)
            {
                return Ok(response.Data);

            }
            return NotFound(response.Message);
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
