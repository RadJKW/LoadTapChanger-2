// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using MudBlazorTest.Server.Models;
using MudBlazorTest.Server.Services.Base;

namespace MudBlazorTest.Server.Services;

public interface IMicrologixPlcService
{
    Task<ServiceResponse<List<DetailsPlcDto>>> Get();

}
