// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ConsoleTestsPLC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ConsoleTestsPLC.Application.Common.Interfaces;
public interface IAppDbContext
{
    DbSet<IntPlcTag> PlcTags { get; }
    DbSet<MicrologixPlc> MicrologixPlcs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
