using MediatR;
using PlcTagLib.Common.Exceptions;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Entities;

namespace PlcTagLib.PlcTags.Commands
{
    public record DeleteTagCommand(int Id) : IRequest;

    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly IPlcTagLibDbContext _context;

        public DeleteTagCommandHandler(IPlcTagLibDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PlcTags.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(PlcTag), request.Id);
            }

            _context.PlcTags.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}


