using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
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
    private IEnumerable<IGrouping<TagTypeId, PlcTagRow>>? TagGroupsList { get; set; }
    private IEnumerable<PlcTagRow>? _selectedPlcsTagList;
    private IEnumerable<PlcDto>? PlcDtosList { get; set; }

    public class PlcTagRow : PlcTag
    {
        public PlcTagRow(PlcTag plcTag, bool enabled = false)
        {
            TagRowEnabled = enabled;
            Address = plcTag.Address;
            SymbolName = plcTag.SymbolName;
            TagTypeId = plcTag.TagTypeId;
            TagType = plcTag.TagType;
            Value = plcTag.Value;
        }

        public bool TagRowEnabled { get; set; }

    }


    private readonly PlcDto _defaultPlc = new PlcDto() { Id = 0, Name = "Select PLC" };

    private PlcDto _selectedPlc = new() { Id = 0, Name = "Select PLC" };


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
        
        var myPlcTagList = tagsFromContext.Select(tag => new PlcTagRow(tag)).ToList();
        
        return myPlcTagList;
        // convert each plctag to MyPlcTag and return the list
        //return tagsFromContext.Select(Mapper.Map<MyPlcTag>).ToList();

    }

    private static string GetListItemStyle(int count)
    {

        return count % 2 == 0 ? "background-color: var(--mud-palette-surface);" :
            // if count is even return 
            "background-color: var(--mud-palette-drawer-background);";
    }

    private static void ToggleTagValue(PlcTag tag)
    {
        tag.Value = !tag.Value;
    }
    
}
