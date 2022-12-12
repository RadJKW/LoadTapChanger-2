using libplctag;
using libplctag.DataTypes;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Common.Interfaces;
using PlcTagLib.Entities;
using DebugLevel=libplctag.DebugLevel;

namespace PlcTagLib.Web.Models;
public class PlcTagRow : PlcTag
{
    private ILogger _logger;
    private IPlcTagLibDbContext _context;

    private MicrologixPlc? _plc;
    private DataTag? _intTag;
    public PlcTagRow(PlcTag plcTag, ILogger logger, IPlcTagLibDbContext context)
    {
        Address = FormatPlcTagAddress(plcTag.Address!);
        PlcId = plcTag.PlcId;
        SymbolName = plcTag.SymbolName;
        TagTypeId = plcTag.TagTypeId;
        TagType = plcTag.TagType;
        BitValue = plcTag.Value;
        _logger = logger;
        _context = context;

    }

    public bool BitValue { get; set; }
    public int IntValue { get; set; }

    public bool TagMonitoringEnabled { get; private set; }

    private new MicrologixPlc? Plc
    {
        get
        {
            return _plc ??= _context.MicrologixPlcs
                .Include(x => x.PlcTags)
                .FirstOrDefault(x => x.Id == PlcId);
        }
        set
        {
            {
                _plc = value;
            }
        }
    }

    public event EventHandler? OnPlcTagValueChanged;

    private void PlcTag_OnValueChanged(object? sender, EventArgs e)
    {
        _logger.LogInformation("PlcTag_OnValueChanged");

        if (!TagMonitoringEnabled)
            TagMonitoringEnabled = true;
        BitValue = _intTag!.Value == 1;
        IntValue = _intTag!.Value;
        OnPlcTagValueChanged?.Invoke(sender, e);
    }

    private string FormatPlcTagAddress(string address)
    {

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




    public class DataTag : Tag<DintPlcMapper, int>
    {
        public event EventHandler? OnTagValueChanged;
        private int _previousHash;
        public DataTag(string tagNameFromPlc, int readInterval, string plcGateway)
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

        _intTag = new DataTag
        (
        tagNameFromPlc: Address!,
        plcGateway: Plc?.IpAddress!,
        readInterval: 50
        );
        _logger.LogInformation($"Tag: {_intTag.Name} --> CREATED");
        _logger.LogInformation($"Tag: {_intTag.Name} --> INITIALIZING");

        _intTag.OnTagValueChanged += PlcTag_OnValueChanged;

        await _intTag.ReadAsync();
        TagMonitoringEnabled = true;

    }

    public Task StopMonitoringAsync()
    {
        TagMonitoringEnabled = false;

        _intTag!.OnTagValueChanged -= PlcTag_OnValueChanged;
        _intTag!.Dispose();
        return Task.CompletedTask;
    }

    public async Task WriteToTagAsync(bool newValue)
    {
        TagMonitoringEnabled = false;
        _intTag!.Value = newValue ? 1 : 0;
        await _intTag.WriteAsync();


    }
}

/*private void ReleaseUnmanagedResources()
{
    // release unmanaged resources here
}
private void Dispose(bool disposing)
{
    ReleaseUnmanagedResources();
    if (disposing)
    {
        DataTag!.Dispose();
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
