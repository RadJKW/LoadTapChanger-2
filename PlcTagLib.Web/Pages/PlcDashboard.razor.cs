using MediatR;
using Microsoft.AspNetCore.Components;
using PlcTagLib.Common.Models;
using PlcTagLib.Entities;
using PlcTagLib.MicrologixPlcs.Commands;
using PlcTagLib.MicrologixPlcs.DTOs;
using PlcTagLib.MicrologixPlcs.Queries;
using PlcTagLib.PlcTags.DTOs;
using PlcTagLib.PlcTags.Queries;

namespace PlcTagLib.Web.Pages;

public partial class PlcDashboard
{
    [Inject]
    protected IMediator Mediator { get; set; } = default!;
    protected TagList PlcTagList { get; set; } = new();
    protected List<PlcTag> PlcTagsToWrite { get; set; } = new();
    protected PlcDto _selectedPlc = new();
    protected IEnumerable<TagDto>? _selectedPlcsTagList;

    protected IEnumerable<PlcDto>? _plcDtosList;

    protected IEnumerable<TagDto>? _tagDtosList;

    // implement MediatR to get the PlcInfo
    protected override async Task OnInitializedAsync()
    {
        var response = await Mediator.Send(new ListAllPlcsCommand());
        _plcDtosList = response.Data!.ToList();
        PlcTagList = await Mediator.Send(new GetTagsQuery());

    }

    void OnValueChanged(PlcDto plc)
    {
        _selectedPlc = plc ?? new PlcDto();
        var tagListForPlcId = PlcTagList.Tags.Where(x => x.PlcId == _selectedPlc.Id).ToList();
        _selectedPlcsTagList = tagListForPlcId ?? null;



        // if the selected plc has PlcTags assigned to it, then get the tags

    }

}
