using AutoMapper;
using MediatR;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.MicrologixPlcs.DTOs;
using PlcTagLib.Common.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace PlcTagLib.MicrologixPlcs.Commands;

// ListAllPlcs
public record ListAllPlcsCommand() : IRequest<ServiceResponse<List<PlcDto>>>;

public class ListAllPlcsCommandHandler : IRequestHandler<ListAllPlcsCommand, ServiceResponse<List<PlcDto>>>
{
    private readonly IPlcTagLibDbContext _context;
    private readonly IMapper _mapper;

    public ListAllPlcsCommandHandler(IPlcTagLibDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<PlcDto>>> Handle(ListAllPlcsCommand request, CancellationToken cancellationToken)
    {
        // get a list of all the plcs from the database 
        var plcs = _context.MicrologixPlcs
            .AsNoTracking()
            .ProjectTo<PlcDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        // return the list of plcs

        return new ServiceResponse<List<PlcDto>>
        {
            Data = await plcs,
            Success = true,
            Message = "List of all plcs"
        };

        // return the list of PlcDto objects


    }
}