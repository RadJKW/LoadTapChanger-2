using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Entities;
using PlcTagLib.Enums;
using PlcTagLib.MicrologixPlcs.Commands;
using PlcTagLib.MicrologixPlcs.DTOs;

namespace PlcTagLib.Web.Pages;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public partial class PlcDashboard : ComponentBase
{
    [Inject] public IMediator Mediator { get; set; } = default!;

    [Inject] public IPlcTagLibDbContext PlcTagLibDbContext { get; set; } = default!;

    // GroupedTagList
    private IEnumerable<IGrouping<TagTypeId, PlcTag>>? TagGroupsList { get; set; }
    private IEnumerable<PlcDto>? PlcDtosList { get; set; }

    private IEnumerable<PlcTag>? _selectedPlcsTagList;

    private readonly PlcDto _defaultPlc = new PlcDto() { Id = 0, Name = "Select PLC" };

    private PlcDto _selectedPlc = new() { Id = 0, Name = "Select PLC" };

    private bool Toggle { get; set; } = false;

    private string _searchText = string.Empty;


    // implement MediatR to get the PlcInfo
    protected override async Task OnInitializedAsync()
    {
        var response = await Mediator.Send(new ListAllPlcsCommand());
        PlcDtosList = response.Data!.ToList();
    }

    private async Task OnValueChanged(PlcDto? plc)
    {
        _selectedPlc = plc ?? _defaultPlc;
        if (_selectedPlc.Id == 0)
        {
            _selectedPlcsTagList = null;
            TagGroupsList = null;
            return;
        }

        _selectedPlcsTagList = await GroupTagsByType(_selectedPlc.Id);
        TagGroupsList = _selectedPlcsTagList.GroupBy(x => x.TagTypeId);
    }

    // create an async Task to update the PlcTagList based on the selected Plc

    private async Task<List<PlcTag>> GroupTagsByType(int plcId)
    {
        if (plcId == 0) return new List<PlcTag>();

        var tagsFromContext = await PlcTagLibDbContext.PlcTags
            .Where(t => t.PlcId == plcId)
            .OrderBy(t => t.TagTypeId)
            .ToListAsync();

        return tagsFromContext;
    }

    private static string GetListItemStyle(int count)
    {

        return count % 2 == 0 ? "background-color: var(--mud-palette-surface);" :
            // if count is even return 
            "background-color: var(--mud-palette-drawer-background);";
    }

    private static Color GetTagState()
    {
        return Color.Dark;
    }

    private static void ToggleTagValue(PlcTag tag)
    {
        tag.Value = !tag.Value;
    }

    private static void FindPlcTagFromList()
    {

    }
}
