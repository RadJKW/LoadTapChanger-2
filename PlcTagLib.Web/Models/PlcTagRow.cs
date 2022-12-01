using PlcTagLib.Common.Interfaces;
using PlcTagLib.Entities;

namespace PlcTagLib.Web.Models;
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
