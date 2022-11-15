// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Data;
using PlcTagLib.Entities;

namespace PlcTagLib.Repositories;
internal class PlcTagRepository : GenericRepository<PlcTag>, IPlcTagRepository

{
    private readonly PlcTagLibDbContext _context;
    private readonly IMapper _mapper;
    public PlcTagRepository(PlcTagLibDbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;

    }

    public async Task<List<PlcTag>?> GetPlcTagsByPlcDeviceIdAsync(int? id)
    {
        if (id == null)
        {
            return null;
        }
        return await _context.PlcTags.Where(x => x.PlcId == id).ToListAsync();
    }
}
