// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace PlcTagLibrary.Models;
public class MicrologixPlc
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gateway { get; set; }
    public string Path { get; set; } = "1,0"; // so far this is always the case, defualts for now

    // this may need to be converted in order to be used
    // with the libplctag library
    public PlcType PlcType { get; set; } = PlcType.Slc500; //defualt to Slc500 for my use case

    // this may need to be converted in order to be used
    // with the libplctag library
    public Protocol Protocol { get; set; } = Protocol.ab_eip; //defualt to ab_eip for my use case

    public TimeSpan Timeout { get; set; } = TimeSpan.FromMilliseconds(5000); //defualt to 5 seconds for my use case
}
