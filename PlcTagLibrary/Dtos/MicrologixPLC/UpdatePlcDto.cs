// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlcTagLibrary.Dtos.MicrologixPLC;
public class UpdatePlcDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? DefaultName { get; set; }

    [Required]
    public string? Gateway { get; set; }
    public short TimeoutSeconds { get; set; }
    public string? Protocol { get; set; }
}
