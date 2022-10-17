// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Services;
public class PlcTagService : IPlcTagService
{


    public Task<IEnumerable<MicrologixTag>> GetTags()
    {
        throw new NotImplementedException();
    }
}

