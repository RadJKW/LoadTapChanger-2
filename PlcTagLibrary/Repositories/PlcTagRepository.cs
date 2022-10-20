// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlcTagLibrary.Data;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Repositories;
internal class PlcTagRepository : GenericRepository<PlcTag>, IPlcTagRepository

{
    private readonly LoadTapChangerDBContext _context;
    private readonly IMapper _mapper;
    public PlcTagRepository(LoadTapChangerDBContext context, IMapper mapper) : base(context, mapper)
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
        return await _context.PlcTags.Where(x => x.PlcDeviceId == id).ToListAsync();
    }
}
