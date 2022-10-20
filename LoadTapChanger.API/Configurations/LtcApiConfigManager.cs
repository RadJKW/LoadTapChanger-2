// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace LoadTapChanger.API.Configurations;

public class LtcApiConfigManager : ILtcApiConfigManager
{
    public string DatabaseConnection => throw new NotImplementedException();

    public IConfigurationSection GetConfigurationSection(string sectionKey)
    {
        throw new NotImplementedException();
    }

    public string GetConnectionString(string connectionName)
    {
        throw new NotImplementedException();
    }
}
