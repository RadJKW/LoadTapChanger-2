﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlcTagLib.Entities;
using PlcTagLib.Common.Mappings;

namespace PlcTagLib.MicrologixPlcs.DTOs;

public class PlcDto : IMapFrom<MicrologixPlc>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? IpAddress { get; set; }
    public string? Location { get; set; }
    public string? Program { get; set; }

}

