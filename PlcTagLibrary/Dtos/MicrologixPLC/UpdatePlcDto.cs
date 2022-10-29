// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Dtos.MicrologixPLC
{
    public class UpdatePlcDto
    {
        public UpdatePlcDto(int id, short? timeoutSeconds)//, Protocol? protocol = null, PlcType? plcType = null)
        {
            Id = id;
            TimeoutSeconds = timeoutSeconds;
            // TimeoutSeconds = timeoutSeconds;
            // Protocol = protocol; // ?? Models.Protocol.ab_eip;
            // PlcType = plcType; // ?? Models.PlcType.MicroLogix;

        }
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; } = "hello";
        public string? Gateway { get; set; }
        public short? TimeoutSeconds { get; set; }
        public Protocol? Protocol { get; set; }

        public PlcType? PlcType { get; set; }
    }
}
