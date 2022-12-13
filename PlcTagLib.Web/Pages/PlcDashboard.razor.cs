using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Enums;
using PlcTagLib.MicrologixPlcs.Commands;
using PlcTagLib.MicrologixPlcs.DTOs;
using PlcTagLib.Web.Models;

namespace PlcTagLib.Web.Pages;
// ReSharper disable once ClassNeverInstantiated.Global
public partial class PlcDashboard : ComponentBase
{
    [Inject] public IMediator Mediator { get; set; } = default!;

    [Inject] public IPlcTagLibDbContext PlcTagLibDbContext { get; set; } = default!;

    [Inject] public ILogger<PlcDashboard> Logger { get; set; } = default!;


    // GroupedTagList
    private PlcDto _selectedPlc = new()
    {
        Id = 0, Name = "Select PLC"
    };

    private readonly PlcDto _defaultPlc = new PlcDto()
    {
        Id = 0, Name = "Select PLC"
    };

    private IEnumerable<PlcTagRow>? _selectedPlcsTagList;

    private IEnumerable<PlcDto>? PlcDtosList { get; set; }
    private IEnumerable<IGrouping<TagTypeId, PlcTagRow>>? TagGroupsList { get; set; }



    private async Task OnValueChanged(PlcDto? plc)
    {
        _selectedPlc = plc ?? _defaultPlc;
        if (_selectedPlc.Id == 0)
        {
            _selectedPlcsTagList = null;
            TagGroupsList = null;
            return;
        }

        _selectedPlcsTagList = await GetTagsFromContextWithType(_selectedPlc.Id);
        TagGroupsList = _selectedPlcsTagList.GroupBy(x => x.TagTypeId);
    }

    // create an async Task to update the PlcTagList based on the selected Plc

    private async Task<List<PlcTagRow>> GetTagsFromContextWithType(int plcId)
    {
        if (plcId == 0) return new List<PlcTagRow>();

        var tagsFromContext = await PlcTagLibDbContext.PlcTags
            .Where(t => t.PlcId == plcId)
            .OrderBy(t => t.TagTypeId)
            .ToListAsync();

        var myPlcTagList = tagsFromContext.Select(tag => new PlcTagRow(tag, Logger, PlcTagLibDbContext)).ToList();

        return myPlcTagList;
        // convert each plctag to MyPlcTag and return the list
        //return tagsFromContext.Select(Mapper.Map<MyPlcTag>).ToList();

    }

    private async Task OnCheckedChanged(bool args, PlcTagRow tagRow)
    {
        // if (args) => StartMonitoringTagAsync();
        // else => StopMonitoringTagAsync();

        Logger.LogInformation("OnCheckedChanged: {Args}", args);

        if (args)
        {
            Console.WriteLine("StartMonitoringTagAsync");
            tagRow.OnPlcTagValueChanged += PlcTagRow_OnTagValueChanged;

            await tagRow.StartMonitoringAsync();
        }
        else
        {
            tagRow.OnPlcTagValueChanged -= PlcTagRow_OnTagValueChanged;
            Console.WriteLine("StopMonitoringTagAsync");
            await tagRow.StopMonitoringAsync();
        }

    }

    // override method to subscribe to plcTagValueChanged event

    private void PlcTagRow_OnTagValueChanged(object? sender, EventArgs e)
    {

        InvokeAsync(StateHasChanged);

    }

    protected override async Task OnInitializedAsync()
    {
        var response = await Mediator.Send(new ListAllPlcsCommand());
        PlcDtosList = response.Data!.ToList();
    }
    protected override void OnAfterRender(bool firstRender)
    {

    }



    // private async Task WriteTagValue(PlcTagRow tagRow)
    // {
    //     await tagRow.WriteToTagAsync(tagRow.Value);
    //
    // }
}
