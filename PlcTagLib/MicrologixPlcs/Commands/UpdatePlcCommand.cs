using AutoMapper;
using MediatR;
using PlcTagLib.Common.Exceptions;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Entities;
using PlcTagLib.MicrologixPlcs.DTOs;

namespace PlcTagLib.MicrologixPlcs.Commands;

public record UpdatePlcCommand(int Id, PlcUpdateDto Dto) : IRequest<PlcDto>;

public class UpdatePlcCommandHandler : IRequestHandler<UpdatePlcCommand, PlcDto>
{
    private readonly IPlcTagLibDbContext _context;
    private readonly IMapper _mapper;

    public UpdatePlcCommandHandler(IPlcTagLibDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PlcDto> Handle(UpdatePlcCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MicrologixPlcs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(MicrologixPlc), request.Id);
        }

        foreach (var entityProperty in entity.GetType().GetProperties())
        {
            var editProperty = request.Dto.GetType().GetProperty(entityProperty.Name);
            if (editProperty != null)
            {
                var editValue = editProperty.GetValue(request.Dto);
                if (editValue != null)
                {
                    entityProperty.SetValue(entity, editValue);
                }
            }
        }

        _ = await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PlcDto>(entity);
    }
}