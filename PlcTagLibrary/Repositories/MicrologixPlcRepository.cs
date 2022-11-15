// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Common.Models;
using PlcTagLib.Data;
using PlcTagLib.Entities;
using PlcTagLib.MicrologixPlcs.DTOs;

namespace PlcTagLib.Repositories;

/* public class MicrologixPlcRepository : GenericRepository<MicrologixPlc>, IMicrologixPlcRepository
 {
     private readonly PlcTagLibDbContext _context;
     private readonly IMapper _mapper;

     public MicrologixPlcRepository(PlcTagLibDbContext context, IMapper mapper) : base(context, mapper)
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

     public async Task<ServiceResponse<DetailsPlcDto>> Update(int id, UpdatePlcDto updatedPlc)
     {
         // TODO: PUT: [Update Plc] should keep previous values if new values are null
         // if the update is successfull, return the readPlcDto of the updated plc

         var response = new ServiceResponse<DetailsPlcDto>();
         try
         {
             // retrieves the plc from the database with the given id
             var plc = await _context.MicrologixPlcs.FirstOrDefaultAsync(q => q.PlcId == id);

             // exit the try if the plc is not found
             if (plc != null)
             {




                 // map the updatedPlc to the plc
                 _mapper.Map(updatedPlc, plc);

                 // update the plc in the database
                 _context.MicrologixPlcs.Update(plc);
                 await _context.SaveChangesAsync();

                 //map the plc to the DetailsPlcDto
                 response.Data = _mapper.Map<DetailsPlcDto>(plc);
             }
         }
         catch (Exception ex)
         {
             response.Success = false;
             response.Message = ex.Message;
         }
         return response;
     }

     public async Task<ServiceResponse<DetailsPlcDto>> Create(CreatePlcDto newPlc)
     {
         var response = new ServiceResponse<DetailsPlcDto>();
         try
         {
             var plc = _mapper.Map<MicrologixPlc>(newPlc);
             _ = await _context.MicrologixPlcs.AddAsync(plc);
             _ = await _context.SaveChangesAsync();
             response.Data = _mapper.Map<DetailsPlcDto>(plc);
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
 }*/
