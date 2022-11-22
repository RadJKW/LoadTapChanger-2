using AutoMapper;
using MediatR;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.MicrologixPlcs.DTOs;
using PlcTagLib.PlcTags.DTOs;
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

// get a list of plcTags from the database where the plcId matches the plcId in the request
public record ListAllPlcTagsForPlcCommand(int PlcId) : IRequest<ServiceResponse<List<TagDto>>>;

public class ListAllPlcTagsForPlcCommandHandler : IRequestHandler<ListAllPlcTagsForPlcCommand, ServiceResponse<List<TagDto>>>
{
    private readonly IPlcTagLibDbContext _context;
    private readonly IMapper _mapper;

    public ListAllPlcTagsForPlcCommandHandler(IPlcTagLibDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<TagDto>>> Handle(ListAllPlcTagsForPlcCommand request, CancellationToken cancellationToken)
    {
        // get a list of plcTags from the database where the plcId matches the plcId in the request
        var plcTags = _context.PlcTags
            .AsNoTracking()
            .Where(x => x.PlcId == request.PlcId)
            .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        // return the list of plcTags

        return new ServiceResponse<List<TagDto>>
        {
            Data = await plcTags,
            Success = true,
            Message = "List of all plcTags for plc"
        };
    }
}

// lets make a ListAllCommand that has options for listing all plcs, plcTags, or both
// this will be a generic command that can be used to list all plcs, plcTags, or both

// the IRequest will be a generic <T> where T is a generic type
// the generic type will be a list of PlcDto, TagDto, or both

// public record ListAllCommand<T>() : IRequest<ServiceResponse<List<T>>>;

// public class ListAllCommandHandler<T> : IRequestHandler<ListAllCommand<T>, ServiceResponse<List<T>>>
//     where T : class
// {
//     private readonly IPlcTagLibDbContext _context;
//     private readonly IMapper _mapper;

//     public ListAllCommandHandler(IPlcTagLibDbContext context, IMapper mapper)
//     {
//         _context = context;
//         _mapper = mapper;
//     }

//     public async Task<ServiceResponse<List<T>>> Handle(ListAllCommand<T> request, CancellationToken cancellationToken)
//     {
//         // determing the type of T and create a switch statement to handle the different types
//         // T will be a list of PlcDto, TagDto, or both

//         // create a list of T
//         var list = new List<T>();

//         // create a switch statement to handle the different types of T

//         switch (typeof(T))
//         {
//             case Type t when t == typeof(PlcDto):
//                 // get a list of all the plcs from the database 
//                 var plcs = _context.MicrologixPlcs
//                     .AsNoTracking()
//                     .ProjectTo<PlcDto>(_mapper.ConfigurationProvider)
//                     .ToListAsync(cancellationToken);

//                 // add the list of plcs to the list of T
//                 list.AddRange((IEnumerable<T>)plcs);

//                 break;

//             case Type t when t == typeof(PlcDetailsDto):
//                 // get a list of all the plcs from the database 
//                 var plcs1 = _context.MicrologixPlcs
//                     .AsNoTracking()
//                     .Include(x => x.PlcTags)
//                     .ProjectTo<PlcDetailsDto>(_mapper.ConfigurationProvider)
//                     .ToListAsync(cancellationToken);

//                 // add the list of plcs to the list of T
//                 list.AddRange((IEnumerable<T>)await plcs1);

//                 break;

//             case Type t when t == typeof(TagDto):
//                 // get a list of all the plcTags from the database 
//                 var plcTags = _context.PlcTags
//                     .AsNoTracking()
//                     .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
//                     .ToListAsync(cancellationToken);

//                 // add the list of plcTags to the list of T
//                 list.AddRange((IEnumerable<T>)await plcTags);

//                 break;

//             // case Type t when t == typeof(PlcDto) && t == typeof(TagDto):
//             //     // get a list of all the plcs from the database 
//             //     var plcs2 = _context.MicrologixPlcs
//             //         .AsNoTracking()
//             //         .ProjectTo<PlcDto>(_mapper.ConfigurationProvider)
//             //         .ToListAsync(cancellationToken);

//             //     // add the list of plcs to the list of T
//             //     list.AddRange(await plcs2 as IEnumerable<T>);

//             //     // get a list of all the plcTags from the database 
//             //     var plcTags2 = _context.PlcTags
//             //         .AsNoTracking()
//             //         .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
//             //         .ToListAsync(cancellationToken);

//             //     // add the list of plcTags to the list of T
//             //     list.AddRange(await plcTags2 as IEnumerable<T>);

//             //     break;

//             default:
//                 break;
//         }

//         // return the list of T

//         return new ServiceResponse<List<T>>
//         {
//             Data = list,
//             Success = true,
//             Message = "List of all plcs"
//         };

//     }
// }
