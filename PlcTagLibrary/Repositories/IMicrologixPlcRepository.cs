// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using PlcTagLibrary.Dtos.MicrologixPLC;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Repositories;

public interface IMicrologixPlcRepository : IGenericRepository<MicrologixPlc>
{
    Task<DetailsPlcDto> GetPlcDetailsAsync(int id);
}
