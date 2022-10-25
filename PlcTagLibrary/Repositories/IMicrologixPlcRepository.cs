// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using PlcTagLibrary.Dtos.MicrologixPLC;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Repositories
{
    public interface IMicrologixPlcRepository : IGenericRepository<MicrologixPlc>
    {
        Task<ServiceResponse<IEnumerable<ReadPlcDto>>> GetAllPlcAsync();

        Task<ServiceResponse<DetailsPlcDto>> GetPlcDetailsAsync(int id);

        Task<ServiceResponse<ReadPlcDto>> GetPlcByIdAsync(int id);

        Task<ServiceResponse<UpdatePlcDto>> UpdatePlcAsync(UpdatePlcDto updatePlcDto);
    }
}
