using MediatR;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Common.Exceptions;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Entities;

namespace PlcTagLib.MicrologixPlcs.Commands;
public record DeletePlcCommand(int Id) : IRequest;

public class DeletePlcCommandHandler : IRequestHandler<DeletePlcCommand>
{
    private readonly IPlcTagLibDbContext _context;

    public DeletePlcCommandHandler(IPlcTagLibDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePlcCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MicrologixPlcs
            .Where(p => p.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(MicrologixPlc), request.Id);
        }

        _context.MicrologixPlcs.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
