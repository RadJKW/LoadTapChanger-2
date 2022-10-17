using MediatR;

namespace ConsoleTestsPLC;

public class LibPlcTagNotification : INotification
{

    public LibPlcTagNotification(short _tag)
    {
        TagValue = _tag;

    }


    public short TagValue { get; set; }
}