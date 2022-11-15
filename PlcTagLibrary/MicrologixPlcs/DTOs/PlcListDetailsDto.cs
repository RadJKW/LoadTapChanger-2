using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlcTagLib.MicrologixPlcs.DTOs;
public class PlcListDetailsDto
{
    public IList<PlcDetailsDto> Plcs { get; set; } = new List<PlcDetailsDto>();
}
