// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using ConsoleTestsPLC.Application.Common.Interfaces;
using ConsoleTestsPLC.Domain.Entities;
using ConsoleTestsPLC.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ConsoleTestsPLC.Infrastructure.Persistence;
public class AppDbContext : DbContext, IAppDbContext
{
    private readonly IMediator _mediator;

    public AppDbContext(
    DbContextOptions<AppDbContext> options,
        IMediator mediator) : base(options)
    {
        _mediator = mediator;


    }
    public DbSet<IntPlcTag> PlcTags => throw new NotImplementedException();

    public DbSet<MicrologixPlc> MicrologixPlcs => throw new NotImplementedException();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //await _mediator.DispatchDomainEventsAsync(this);

        try
        {

            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new DbUpdateConcurrencyException(ex.Message, ex);
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

    }

}
