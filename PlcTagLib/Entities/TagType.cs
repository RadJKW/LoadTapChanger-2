using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlcTagLib.Enums;

namespace PlcTagLib.Entities;
public partial class TagType
{
    public TagTypeId TagTypeId { get; set; }
    public string? Name { get; set; }


}
