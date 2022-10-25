// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PlcTagLibrary.Data;
using PlcTagLibrary.Dtos.MicrologixPLC;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Repositories;

public class MicrologixPlcRepository : GenericRepository<MicrologixPlc>, IMicrologixPlcRepository
{
    private readonly LoadTapChangerDBContext _context;
    private readonly IMapper _mapper;

    public MicrologixPlcRepository(LoadTapChangerDBContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<IEnumerable<ReadPlcDto>>> List()
    {
        var response = new ServiceResponse<IEnumerable<ReadPlcDto>>();
        try
        {
            var plc = await _context.MicrologixPlcs.ToListAsync();
            var plcDto = _mapper.Map<IEnumerable<ReadPlcDto>>(plc);
            response.Data = plcDto;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }
        return response;
    }

    public async Task<ServiceResponse<DetailsPlcDto>> GetDetailsById(int id)
    {
        var response = new ServiceResponse<DetailsPlcDto>();
        try
        {
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

    public async Task<ServiceResponse<ReadPlcDto>> GetById(int id)
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

    public async Task<ServiceResponse<UpdatePlcDto>> Update(UpdatePlcDto updatedPlc)
    {
        // TODO: This no work I think. Need to implement the GetPlcById method
        var response = new ServiceResponse<UpdatePlcDto>();
        try
        {
            var plc = await _context.MicrologixPlcs
                .ProjectTo<UpdatePlcDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == updatedPlc.Id);

            if (plc != null)
            {
                _mapper.Map(updatedPlc, plc);
                await _context.SaveChangesAsync();

                response.Data = updatedPlc;
            }
            else
            {
                response.Success = false;
                response.Message = "Plc not found";
            }
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }
        return response;
    }

    public async Task<ServiceResponse<CreatePlcDto>> Create(CreatePlcDto newPlc)
    {
        // add the newPlc to the database

        var response = new ServiceResponse<CreatePlcDto>();
        try
        {
            var plc = _mapper.Map<MicrologixPlc>(newPlc);
            await _context.MicrologixPlcs.AddAsync(plc);

            response.Data = newPlc;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }
        return response;

    }

    public async Task<ServiceResponse<IEnumerable<DetailsPlcDto>>> ListDetails()
    {
        var response = new ServiceResponse<IEnumerable<DetailsPlcDto>>();
        try
        {
            var plcDetailsList = await _context.MicrologixPlcs
                .Include(p => p.PlcTags)
                .ProjectTo<DetailsPlcDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            response.Data = plcDetailsList;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }
        return response;

    }

    public Task<ServiceResponse<ReadPlcDto>> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
