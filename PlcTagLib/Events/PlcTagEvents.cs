// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using PlcTagLib.Entities;

namespace PlcTagLib.Events;
public class PlcTagCreatedEvent : BaseEvent
{
    public PlcTagCreatedEvent(PlcTag plcTag)
    {
        PlcTag = plcTag;
    }

    public PlcTag PlcTag { get; }
}
public class PlcTagValueChangedEvent : BaseEvent
{
    public PlcTagValueChangedEvent(PlcTag plcTag)
    {
        PlcTag = plcTag;
    }

    public PlcTag PlcTag { get; }
}

public class PlcTagDeletedEvent : BaseEvent
{
    public PlcTagDeletedEvent(PlcTag plcTag)
    {
        PlcTag = plcTag;
    }

    public PlcTag PlcTag { get; }
}


