// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using AutoMapper;
using PlcTagLibrary.Dtos.MicrologixPLC;
using PlcTagLibrary.Dtos.PlcTag;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        // map the ReadPlcDto.Id to MicrologixPlc.PlcId
        CreateMap<ReadPlcDto, MicrologixPlc>()
            .ForMember(dest => dest.PlcId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<ReadPlcTagDto, PlcTag>()

            .ForMember(dest => dest.TagId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<CreatePlcTagDto, PlcTag>()
            .ReverseMap();

        CreateMap<UpdatePlcTagDto, PlcTag>()
            .ReverseMap();
    }
}
