// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using PlcTagLibrary.Dtos.PlcTag;

namespace PlcTagLibrary.Dtos.MicrologixPLC
{
    public class DetailsPlcDto : ReadPlcDto
    {


        public string? DefaultName { get; set; }
        public short TimeoutSeconds { get; set; }
        public string? Protocol { get; set; }

        public List<ReadPlcTagDto>? PlcTags { get; set; }
    }
}