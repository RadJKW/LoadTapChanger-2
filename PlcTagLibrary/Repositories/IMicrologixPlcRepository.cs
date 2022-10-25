// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using PlcTagLibrary.Dtos.MicrologixPLC;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Repositories
{
    /// <summary>
    /// Interface for MicrologixPlcRepository
    /// </summary>
    /// 
    public interface IMicrologixPlcRepository : IGenericRepository<MicrologixPlc>
    {


        // ----- CreatePlcDto ------
        Task<ServiceResponse<CreatePlcDto>> Create(CreatePlcDto newPlc);
        // ----- ReadPlcDto --------
        Task<ServiceResponse<ReadPlcDto>> GetById(int id);
        Task<ServiceResponse<IEnumerable<ReadPlcDto>>> List();
        // ----- DetailsPlcDto -----
        Task<ServiceResponse<IEnumerable<DetailsPlcDto>>> ListDetails();
        Task<ServiceResponse<DetailsPlcDto>> GetDetailsById(int id);
        // ----- UpdatePlcDto ------
        Task<ServiceResponse<UpdatePlcDto>> Update(UpdatePlcDto updatedPlc);
        // ----- DeletePlcDto ------
        Task<ServiceResponse<ReadPlcDto>> Delete(int id);



    }
}
