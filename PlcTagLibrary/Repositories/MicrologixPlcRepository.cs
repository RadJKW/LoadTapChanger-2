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

    public async Task<DetailsPlcDto> GetPlcDetailsAsync(int id)
    {
        var plc = await _context.MicrologixPlcs
                    .Include(plc => plc.PlcId)
                    .ProjectTo<DetailsPlcDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(q => q.Id == id);

        return plc!;

    }
}

