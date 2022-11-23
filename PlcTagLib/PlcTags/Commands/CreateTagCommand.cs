using AutoMapper;
using MediatR;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Entities;

namespace PlcTagLib.PlcTags.Commands;

public record CreateTagCommand : IRequest<int>
{
    public string? SymbolName { get; set; }

    public string? Address { get; set; }
    public string? Description { get; set; }

    //public int TagType { get; set; }


}

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
{
    private readonly IPlcTagLibDbContext _context;

    public CreateTagCommandHandler(IPlcTagLibDbContext context, IMapper mapper)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var entity = new PlcTag
        {
            SymbolName = request.SymbolName,
            Address = request.Address,
            Description = request.Description,

        };

        _context.PlcTags.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}



