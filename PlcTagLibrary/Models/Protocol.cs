// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlcTagLibrary.Models;
public enum Protocol
{
    /// <summary>
    /// Allen-Bradley specific flavor of EIP
    /// </summary>
    ab_eip,

    /// <summary>
    /// A Modbus TCP implementation used by many PLCs
    /// </summary>
    modbus_tcp
}
