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
        private ILogger Logger { get; init; }
        private IPlcTagLibDbContext Context { get; init; }

        private MicrologixPlc? _plc;
        private IntTag? _intTag;
        public PlcTagRow(PlcTag plcTag, ILogger logger, IPlcTagLibDbContext context)
        {
            Address = FormatPlcTagAddress(plcTag.Address!);
            PlcId = plcTag.PlcId;
            SymbolName = plcTag.SymbolName;
            TagTypeId = plcTag.TagTypeId;
            TagType = plcTag.TagType;
            Value = plcTag.Value;
            Logger = logger;
            Context = context;

        }

        public bool TagMonitoringEnabled { get; private set; }

        private new MicrologixPlc? Plc
        {
            get
            {
                return _plc ??= Context.MicrologixPlcs
                    .Include(x => x.PlcTags)
                    .FirstOrDefault(x => x.Id == PlcId);
            }
            set
            {
                { _plc = value; }
            }
        }

        public event EventHandler? OnPlcTagValueChanged;

        private void PlcTag_OnValueChanged(object? sender, EventArgs e)
        {
            Console.WriteLine("PlcTag_OnValueChanged");
            // set the value of this.Value to the value from eventArgs
            Value = _intTag!.Value == 1;
            OnPlcTagValueChanged?.Invoke(sender, e);
        }

        private string FormatPlcTagAddress(string address)
        {
            // if address first char is I, then address.Insert(1,"1")
            // if address firs char is O, then address.Insert(1,"0")
            // else do nothing and return address

            if (address[0] == 'I')
            {
                return address.Insert(1, "1");
            }

            if (address[0] == 'O')
            {
                return address.Insert(1, "0");
            }


            return address;

        }




        public class IntTag : Tag<DintPlcMapper, int>
        {
            public event EventHandler? OnTagValueChanged;
            private int _previousHash;
            public IntTag(string tagNameFromPlc, int readInterval, string plcGateway)
            {
                Name = tagNameFromPlc;
                Gateway = plcGateway;
                Path = "1,0";
                PlcType = libplctag.PlcType.Slc500;
                Protocol = libplctag.Protocol.ab_eip;
                AutoSyncReadInterval = TimeSpan.FromMilliseconds(readInterval);

                ReadCompleted += IntTag_OnTagValueChanged;
            }
            private void IntTag_OnTagValueChanged(object? sender, TagEventArgs tagEventArgs)
            {

                var hash = this.Value!.GetHashCode();
                if (hash != _previousHash)
                {
                    OnTagValueChanged?.Invoke(this, EventArgs.Empty);
                }
                _previousHash = hash;
            }

        }

        public async Task StartMonitoringAsync()
        {
            TagMonitoringEnabled = true;
            _intTag = new IntTag
            (
                tagNameFromPlc: Address!,
                plcGateway: Plc?.IpAddress!,
                readInterval: 100
            );
            Console.WriteLine($"Tag: {_intTag.Name} --> CREATED");
            Console.WriteLine($"Tag: {_intTag.Name} --> INITIALIZING");

            _intTag.OnTagValueChanged += PlcTag_OnValueChanged;
            await _intTag.InitializeAsync();

        }

        public Task StopMonitoringAsync()
        {
            TagMonitoringEnabled = false;

            _intTag!.OnTagValueChanged -= PlcTag_OnValueChanged;
            _intTag!.Dispose();
            return Task.CompletedTask;
        }
        /*private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }
        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                IntTag!.Dispose();
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~PlcTagRow()
        {
            Dispose(false);
        }*/
    }

    private readonly PlcDto _defaultPlc = new PlcDto()
    {
        Id = 0,
        Name = "Select PLC"
    };
    private PlcDto _selectedPlc = new()
    {
        Id = 0,
        Name = "Select PLC"
    };



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

    public async Task OnCheckedChanged(bool args, PlcTagRow tagRow)
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


}
