using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlcTagLib.Entities;
public class TagEnum
{
    public int TagType { get; set; }
    public string? Name { get; set; }

}
public enum TagType : int
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
