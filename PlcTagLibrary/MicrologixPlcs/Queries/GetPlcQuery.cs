using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Common.Mappings;
using PlcTagLib.MicrologixPlcs.DTOs;

namespace PlcTagLib.MicrologixPlcs.Queries;
public record GetPlcQuery(int Id) : IRequest<PlcListDetailsDto>;
public class GetPlcQueryHandler : IRequestHandler<GetPlcQuery, PlcListDetailsDto>
{
    private readonly IPlcTagLibDbContext _context;
    private readonly IMapper _mapper;

    public GetPlcQueryHandler(IPlcTagLibDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PlcListDetailsDto> Handle(GetPlcQuery request, CancellationToken cancellationToken)
    {
        return new PlcListDetailsDto
        {
            Plcs = await _context.MicrologixPlcs
                .AsNoTracking()
                .Include(p => p.PlcTags.Where(p => p.PlcId == request.Id))
                .Where(p => p.Id == request.Id)
                .ProjectToListAsync<PlcDetailsDto>(_mapper.ConfigurationProvider)
        };
    }
}
