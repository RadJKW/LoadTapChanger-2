using AutoMapper;
using MediatR;
using PlcTagLib.Common.Exceptions;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Entities;
using PlcTagLib.PlcTags.DTOs;

namespace PlcTagLib.PlcTags.Commands
{
    public record UpdateTagCommand(int Id, TagUpdateDto Dto) : IRequest<TagDto>;

    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, TagDto>
    {
        private readonly IPlcTagLibDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTagCommandHandler(IPlcTagLibDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TagDto> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PlcTags.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(PlcTag), request.Id);
            }

            _mapper.Map(request.Dto, entity);

            /*foreach (var entityProperty in entity.GetType().GetProperties())
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
            }*/

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TagDto>(entity);
        }
    }



}


