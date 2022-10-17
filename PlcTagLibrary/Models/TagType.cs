// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace PlcTagLibrary.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TagType
{
    Output = 0,
    Input = 1,
    Binary = 3

}
