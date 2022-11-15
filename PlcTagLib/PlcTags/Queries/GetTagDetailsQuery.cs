using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.PlcTags.DTOs;

namespace PlcTagLib.PlcTags.Queries;

public record GetTagDetailsQuery(int Id) : IRequest<TagDetailsDto>;

public class GetTagDetailsQueryHandler : IRequestHandler<GetTagDetailsQuery, TagDetailsDto>
{
    private readonly IPlcTagLibDbContext _context;
    private readonly IMapper _mapper;

    public GetTagDetailsQueryHandler(IPlcTagLibDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TagDetailsDto> Handle(GetTagDetailsQuery request, CancellationToken cancellationToken)
    {
        return await _context.PlcTags
            .Where(p => p.Id == request.Id)
            .ProjectTo<TagDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken) ?? new TagDetailsDto();
    }
}





