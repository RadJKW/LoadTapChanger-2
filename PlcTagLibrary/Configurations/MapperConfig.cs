// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using AutoMapper;
using Microsoft.Data.SqlClient;
using PlcTagLibrary.Dtos.MicrologixPLC;
using PlcTagLibrary.Dtos.PlcTag;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {


        // 1A: this addresses null TimeoutSeconds when updating Plc
        // 1A: uses the destinations value if src is null. 
        CreateMap<short?, short>().ConvertUsing((src, dest) => src ?? dest);


        CreateMap<ReadPlcDto, MicrologixPlc>()
            .ForMember(dest => dest.PlcId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<DetailsPlcDto, MicrologixPlc>()
            .ForMember(dest => dest.PlcId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<CreatePlcDto, MicrologixPlc>()
            .ReverseMap();

        // for the UpdatePlcDto,
        // if any of the source properties are null,
        // then don't map them to the destination properties

        CreateMap<UpdatePlcDto, MicrologixPlc>()
            // 1B: this is also necessary for null TimeoutSeconds to work. 
            .ForAllMembers(opts =>
            {
                opts.Condition((src, dest, srcMember) => srcMember != null);
            });

        CreateMap<UpdatePlcDto, DetailsPlcDto>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ReadPlcTagDto, PlcTag>()
            .ForMember(dest => dest.TagId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<DetailsPlcTagDto, PlcTag>()
            .ForMember(dest => dest.TagId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<CreatePlcTagDto, PlcTag>()
            .ReverseMap();

        CreateMap<UpdatePlcTagDto, PlcTag>()
            .ReverseMap();
    }
}


/*
 *  
 {
  "id": 3,
  "name": "set-time",
  "timeoutSeconds": 10
 }

 {
  "id": 3,
  "name": "null-time"
 }
 *
 */
