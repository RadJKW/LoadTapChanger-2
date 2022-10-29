// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using PlcTagLibrary.Dtos.PlcTag;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Dtos.MicrologixPLC
{
    public class DetailsPlcDto
    {

        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? DefaultName { get; set; }

        public string? Gateway { get; set; }

        public string? PlcType { get; set; }
        public short? TimeoutSeconds { get; set; }
        public string? Protocol { get; set; }

        public List<ReadPlcTagDto>? PlcTags { get; set; }
    }
}
