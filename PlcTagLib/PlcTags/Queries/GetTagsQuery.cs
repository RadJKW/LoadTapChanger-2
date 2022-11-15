using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.PlcTags.DTOs;

namespace PlcTagLib.PlcTags.Queries
{



    public record GetTagsQuery : IRequest<TagList>;
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, TagList>
    {
        private readonly IPlcTagLibDbContext _context;
        private readonly IMapper _mapper;

        public GetTagsQueryHandler(IPlcTagLibDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TagList> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            return new TagList
            {
                Tags = await _context.PlcTags
                    .AsNoTracking()
                    .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }

}


