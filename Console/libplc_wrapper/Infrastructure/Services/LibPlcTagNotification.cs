using MediatR;

namespace ConsoleTestsPLC.Infrastructure.Services;

public class LibPlcTagNotification : INotification
{

    public LibPlcTagNotification(short tag)
    {
        TagValue = tag;

    }


    public short TagValue { get; set; }
}
