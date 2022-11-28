using libplctag;
using libplctag.DataTypes;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Entities;
using PlcTagLib.Enums;
using PlcTagLib.MicrologixPlcs.Commands;
using PlcTagLib.MicrologixPlcs.DTOs;

namespace PlcTagLib.Web.Pages;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class PlcDashboard : ComponentBase
{
    [Inject] public IMediator Mediator { get; set; } = default!;

    [Inject] public IPlcTagLibDbContext PlcTagLibDbContext { get; set; } = default!;
    
    [Inject] public ILogger<PlcDashboard> Logger { get; set; } = default!;
  

    // GroupedTagList
    private IEnumerable<IGrouping<TagTypeId, PlcTagRow>>? TagGroupsList { get; set; }
    private IEnumerable<PlcTagRow>? _selectedPlcsTagList;
    private IEnumerable<PlcDto>? PlcDtosList { get; set; }

    public class PlcTagRow : PlcTag
    {
        private ILogger Logger { get; set; }
        private IPlcTagLibDbContext Context { get; set; }
        public PlcTagRow(PlcTag plcTag, ILogger logger, IPlcTagLibDbContext context, bool enabled = false)
        {
            TagRowEnabled = enabled;
            Address = plcTag.Address;
            PlcId = plcTag.PlcId;
            SymbolName = plcTag.SymbolName;
            TagTypeId = plcTag.TagTypeId;
            TagType = plcTag.TagType;
            Value = plcTag.Value;
            Logger = logger;
            Context = context;
            
        }

        public bool TagRowEnabled { get; set; }
        

    }


    private readonly PlcDto _defaultPlc = new PlcDto()
    {
        Id = 0, Name = "Select PLC"
    };

    private PlcDto _selectedPlc = new()
    {
        Id = 0, Name = "Select PLC"
    };


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

        var myPlcTagList = tagsFromContext.Select(tag => new PlcTagRow(tag, Logger, PlcTagLibDbContext)).ToList();

        return myPlcTagList;
        // convert each plctag to MyPlcTag and return the list
        //return tagsFromContext.Select(Mapper.Map<MyPlcTag>).ToList();

    }
    
    
    private class TagValueNotifier<M,T> : Tag<M,T> where M: IPlcMapper<T>, new()
        {
            public event EventHandler? ValueChanged;
            private int _previousHash;
            public TagValueNotifier()
            {
                ReadCompleted += OnValueChanged!;
            }
            private void OnValueChanged(object sender, TagEventArgs e)
            {
                var currentHash = this.Value!.GetHashCode();
                if (currentHash != _previousHash)
                {
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
                _previousHash = currentHash;
            }
        }

    private async Task StartMonitoringTagValueAsync(PlcTagRow plcTagRow)
        {
            if (!plcTagRow.TagRowEnabled) return;
        
            Logger.LogInformation("StartMonitoringTagValueAsync");
            plcTagRow.TagRowEnabled = true;
            var plc = await PlcTagLibDbContext.MicrologixPlcs.FindAsync(plcTagRow.PlcId);
            var tag = new TagValueNotifier<DintPlcMapper, int>()
            {
                // insert "1" after "I" in plcTag.Address to read the value
                Name = plcTagRow.Address!.Insert(1, "1"),
                Gateway = plc!.IpAddress,
                Path = "1,0",
                PlcType = libplctag.PlcType.Slc500,
                Protocol = libplctag.Protocol.ab_eip,
                AutoSyncReadInterval = TimeSpan.FromMilliseconds(50)
            };
        
           await tag.InitializeAsync();

           Console.WriteLine($"Tag Name: {tag.Name}");
            Console.WriteLine($"Tag Gateway: {tag.Gateway}");
            Console.WriteLine($"Tag Path: {tag.Path}");
            Console.WriteLine($"Tag PlcType: {tag.PlcType}");
            Console.WriteLine($"Tag Protocol: {tag.Protocol}");
            Console.WriteLine($"Tag Value: {tag.Value}");
            tag.ValueChanged += (sender, args) =>
            {
                
                plcTagRow.Value = tag.Value == 1;
                InvokeAsync(StateHasChanged);
                Console.WriteLine($"Tag Value: {tag.Value}");
            };
        }
}

    

