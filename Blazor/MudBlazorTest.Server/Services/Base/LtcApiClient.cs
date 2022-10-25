// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection.Metadata.Ecma335;

namespace MudBlazorTest.Server.Services.Base;

public class LtcApiClient : ILtcApiClient
{
    private HttpClient _httpClient;

    public LtcApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


}
