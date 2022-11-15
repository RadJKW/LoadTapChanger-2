using MediatR;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Entities;

namespace PlcTagLib.MicrologixPlcs.Commands;
public record CreatePlcCommand : IRequest<int>
{
    public string? Name { get; init; }
    public string? IpAddress { get; init; }
    public string? Location { get; init; }
    public string? Description { get; init; }
}
public class CreatePlcCommandHandler : IRequestHandler<CreatePlcCommand, int>
{
    private readonly IPlcTagLibDbContext _context;

    public CreatePlcCommandHandler(IPlcTagLibDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePlcCommand request, CancellationToken cancellationToken)
    {
        var entity = new MicrologixPlc
        {
            Name = request.Name,
            IpAddress = request.IpAddress,
            Location = request.Location,
            Description = request.Description
        };

        _context.MicrologixPlcs.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
