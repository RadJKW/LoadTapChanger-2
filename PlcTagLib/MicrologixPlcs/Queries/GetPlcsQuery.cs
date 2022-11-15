using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.MicrologixPlcs.DTOs;

namespace PlcTagLib.MicrologixPlcs.Queries;
public record GetPlcsQuery : IRequest<PlcList>;
public class GetPlcsQueryHandler : IRequestHandler<GetPlcsQuery, PlcList>
{
    private readonly IPlcTagLibDbContext _context;
    private readonly IMapper _mapper;

    public GetPlcsQueryHandler(IPlcTagLibDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PlcList> Handle(GetPlcsQuery request, CancellationToken cancellationToken)
    {
        return new PlcList
        {
            Plcs = await _context.MicrologixPlcs
                .AsNoTracking()
                .ProjectTo<PlcDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}
