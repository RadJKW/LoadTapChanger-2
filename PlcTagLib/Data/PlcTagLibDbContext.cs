using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Common.Extensions;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Data.Interceptors;
using PlcTagLib.Entities;

namespace PlcTagLib.Data;

public class PlcTagLibDbContext : DbContext, IPlcTagLibDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public PlcTagLibDbContext(
        DbContextOptions<PlcTagLibDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }


    public DbSet<MicrologixPlc> MicrologixPlcs => Set<MicrologixPlc>();

    public DbSet<PlcTag> PlcTags => Set<PlcTag>();

    public DbSet<TagType> TagTypes => Set<TagType>();



    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
    
}
