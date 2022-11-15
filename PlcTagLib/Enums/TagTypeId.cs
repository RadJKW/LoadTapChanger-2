namespace PlcTagLib.Enums;

public enum TagTypeId : int
{
    Output = 0,
    Input = 1,
    Status = 2,
    Binary = 3,
    Timer = 4,
    Counter = 5,
    Control = 6,
    Integer = 7,
    Float = 8,
    Unknown = 99
}
