// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace LoadTapChanger.API.Configurations;

public interface ILtcApiConfigManager
{
    string DatabaseConnection { get; }

    string GetConnectionString(string connectionName);

    IConfigurationSection GetConfigurationSection(string sectionKey);
}
