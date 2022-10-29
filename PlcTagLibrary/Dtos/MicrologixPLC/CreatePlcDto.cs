// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;

namespace PlcTagLibrary.Dtos.MicrologixPLC
{
    public class CreatePlcDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Gateway { get; set; }

    }
}