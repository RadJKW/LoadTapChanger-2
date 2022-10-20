// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlcTagLibrary.Dtos.PlcTag;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Dtos.MicrologixPLC;
public class DetailsPlcDto : ReadPlcDto
{


    public string? DefaultName { get; set; }
    public short TimeoutSeconds { get; set; }
    public Protocol Protocol { get; set; }

    public List<ReadPlcTagDto>? PlcTags { get; set; }
}
