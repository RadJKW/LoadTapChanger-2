﻿// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MudBlazorTest.Server.Services.Base;

public partial interface IClient
{
    public HttpClient HttpClient { get; }
}