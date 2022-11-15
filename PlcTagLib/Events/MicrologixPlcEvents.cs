// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlcTagLib.Entities;

namespace PlcTagLib.Events;
public class MicrologixPlcCreatedEvent
{
    public MicrologixPlcCreatedEvent(MicrologixPlc micrologixPlc)
    {
        MicrologixPlc = micrologixPlc;
    }

    public MicrologixPlc MicrologixPlc { get; }
}

public class MicrologixPlcDeletedEvent
{
    public MicrologixPlcDeletedEvent(MicrologixPlc micrologixPlc)
    {
        MicrologixPlc = micrologixPlc;
    }

    public MicrologixPlc MicrologixPlc { get; }
}


