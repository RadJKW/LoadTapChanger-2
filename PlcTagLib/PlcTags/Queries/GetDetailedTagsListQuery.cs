using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.PlcTags.DTOs;

namespace PlcTagLib.PlcTags.Queries;
public record GetDetailedTagsListQuery(int PlcId) : IRequest<List<TagDetailsDto>>;

public class GetDetailedTagsListQueryHandler : IRequestHandler<GetDetailedTagsListQuery, List<TagDetailsDto>>
    {
        private readonly IPlcTagLibDbContext _context;
        private readonly IMapper _mapper;
    
        public GetDetailedTagsListQueryHandler(IPlcTagLibDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
        public async Task<List<TagDetailsDto>> Handle(GetDetailedTagsListQuery request, CancellationToken cancellationToken)
        {
            return await _context.PlcTags
                .Where(t => t.PlcId == request.PlcId)
                .ProjectTo<TagDetailsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }


