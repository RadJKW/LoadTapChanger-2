// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PlcTagLibrary.Data;
using PlcTagLibrary.Dtos.MicrologixPLC;
using PlcTagLibrary.Dtos.PlcTag;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Repositories
{
    public class MicrologixPlcRepository : GenericRepository<MicrologixPlc>, IMicrologixPlcRepository
    {
        private readonly LoadTapChangerDBContext _context;
        private readonly IMapper _mapper;

        public MicrologixPlcRepository(LoadTapChangerDBContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<ReadPlcDto>>> GetAllPlcAsync()
        {
            var response = new ServiceResponse<IEnumerable<ReadPlcDto>>();
            try
            {
                var plc = await _context.MicrologixPlcs.ToListAsync();
                var plcDto = _mapper.Map<IEnumerable<ReadPlcDto>>(plc);
                response.Data = plcDto;
            }
            //return the error so the controller can handle it
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

            }
            return response;
        }

        public async Task<ServiceResponse<DetailsPlcDto>> GetPlcDetailsAsync(int id)
        {
            var response = new ServiceResponse<DetailsPlcDto>();
            try
            {
                //get the plc by id
                // include every plcTag where the PlcDeviceId = plc.id
                var plc = await _context.MicrologixPlcs
                    .Include(p => p.PlcTags)
                    .ProjectTo<DetailsPlcDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(p => p.Id == id);

                response.Data = plc;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<ServiceResponse<ReadPlcDto>> GetPlcByIdAsync(int id)
        {
            var response = new ServiceResponse<ReadPlcDto>();
            try
            {
                var plc = await _context.MicrologixPlcs
                    .ProjectTo<ReadPlcDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(q => q.Id == id);
                response.Data = plc;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;


        }
    }
}
