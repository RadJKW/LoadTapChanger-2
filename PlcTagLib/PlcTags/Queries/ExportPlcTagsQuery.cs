using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Common.Models;
using PlcTagLib.PlcTags.DTOs;

namespace PlcTagLib.PlcTags.Queries;

public record ExportPlcTagsQuery(int PlcId) : IRequest<CsvFileVm>;

public class ExportPlcTagsQueryHandler : IRequestHandler<ExportPlcTagsQuery, CsvFileVm>
{
    private readonly IPlcTagLibDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICsvService _csvService;

    public ExportPlcTagsQueryHandler(IPlcTagLibDbContext context, IMapper mapper, ICsvService csvService)
    {
        _context = context;
        _mapper = mapper;
        _csvService = csvService;
    }

    public async Task<CsvFileVm> Handle(ExportPlcTagsQuery request, CancellationToken cancellationToken)
    {
        var records = await _context.PlcTags
            .Where(p => p.PlcId == request.PlcId)
            .ProjectTo<TagRecord>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var vm = new CsvFileVm
        {
            FileName = "PlcTags.csv",
            ContentType = "text/csv",
            Content = _csvService.BuildPlcTagsFile(records)
        };

        return vm;
    }
}



