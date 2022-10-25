// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using MudBlazorTest.Server.Models;
using MudBlazorTest.Server.Services.Base;
namespace MudBlazorTest.Server.Services;

public class MicrologixPlcService : IMicrologixPlcService
{
    private readonly IClient _client;
    private readonly ILogger<MicrologixPlcService> _logger;

    public MicrologixPlcService(IClient client, ILogger<MicrologixPlcService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<ServiceResponse<List<ReadPlcDto>>> Get()
    {
        ServiceResponse<List<ReadPlcDto>> response;
        try
        {
            var data = await _client.MicrologixPlcsAllAsync();
            response = new ServiceResponse<List<ReadPlcDto>>
            {
                Data = data.ToList(),
                Success = true,
            };

        }
        catch (LtcApiException exception)
        {

            response = new ServiceResponse<List<ReadPlcDto>>
            {
                Data = null,
                Success = false,
                Message = exception.Message,
            };
        }

        return response;
    }
}
