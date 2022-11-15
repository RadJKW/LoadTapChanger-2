// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using PlcTagLib.Entities;

namespace PlcTagLib.Common.Interfaces;

public interface IRsLogixDbImporter
{
    // return the list of PlcTags
    List<PlcTag> PlcTags { get; }
    public void Convert(Uri csvFilePath, Uri jsonFilePath, int addressColumn, int symbolColumn, int[] descriptionColumns, MicrologixPlc plc);

}
