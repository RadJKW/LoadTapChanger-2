// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using PlcTagLibrary.Models;
namespace PlcTagLibrary.Dtos.MicrologixPLC;

public class ReadPlcDto : BaseDto
{
    public string? Name { get; set; }
    public string? Gateway { get; set; }
    public string? PlcType { get; set; }
}
