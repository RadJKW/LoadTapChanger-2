
using ConsoleTestsPLC.Application.Common.Interfaces;

namespace ConsoleTestsPLC.Infrastructure.Services;
public class DateTimeServices : IDateTime
{
    public DateTime Now => DateTime.Now;
}
