using Microsoft.AspNetCore.Mvc;
using PlcTagLibrary.Data;
using PlcTagLibrary.Dtos.MicrologixPLC;
using PlcTagLibrary.Models;
using PlcTagLibrary.Repositories;


// TODO: Figure out how to add form inputs to swaggerUI
// TODO: PUT: [Update Plc] should keep previous values if new values are null


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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadPlcDto>>> GetPlcs()
        {
            var response = await _plcRepository.List();
            return response.Success ? (ActionResult<IEnumerable<ReadPlcDto>>)Ok(response.Data) : (ActionResult<IEnumerable<ReadPlcDto>>)NotFound(response.Message);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<MicrologixPlc>>> GetPlc(int id)
        {
            return Ok(await _plcRepository.GetById(id));
        }

        [HttpGet("details/", Name = "GetAllPlcDetails")]
        public async Task<ActionResult<IEnumerable<DetailsPlcDto>>> GetAllPlcDetails()
        {
            var response = await _plcRepository.ListDetails();
            return response.Success ? (ActionResult<IEnumerable<DetailsPlcDto>>)Ok(response.Data) : (ActionResult<IEnumerable<DetailsPlcDto>>)NotFound(response.Message);
        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<ServiceResponse<MicrologixPlc>>> GetPlcDetails(int id)
        {
            return Ok(await _plcRepository.GetDetailsById(id));
        }



        [HttpPut("{id}", Name = "UpdatePlc")]
        public async Task<ActionResult<DetailsPlcDto>> UpdatePlc(int id, UpdatePlcDto updatePlcDto)
        {
            var response = await _plcRepository.Update(id, updatePlcDto);
            return response.Success ? (ActionResult<DetailsPlcDto>)Ok(response.Data) : (ActionResult<DetailsPlcDto>)NotFound(response.Message);
        }


        [HttpPost]
        public async Task<ActionResult<DetailsPlcDto>> AddPlc(CreatePlcDto newPlc)
        {
            var response = await _plcRepository.Create(newPlc);
            return response.Success ? (ActionResult<DetailsPlcDto>)Ok(response.Data) : (ActionResult<DetailsPlcDto>)NotFound(response.Message);
        }

        // TODO: Remove Data Access, Use Repository
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

            return Ok();
        }
    }
}
