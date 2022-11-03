// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ConsoleTestsPLC.Domain.Common;
using ConsoleTestsPLC.Domain.Entities;
using ConsoleTestsPLC.Domain.Enums;
using libplctag;
using libplctag.DataTypes;
namespace ConsoleTestsPLC.Domain.Entities;

// since
public class IntPlcTag : BaseAuditableEntity
{
    public int PlcId { get; set; }
    public string? SymbolName { get; set; }
    public string? Address { get; set; }

    public string? Description { get; set; }

    public MicrologixPlc Plc { get; set; } = null!;

}

