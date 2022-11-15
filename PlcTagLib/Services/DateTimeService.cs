using PlcTagLib.Common.Interfaces;

namespace PlcTagLib.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
