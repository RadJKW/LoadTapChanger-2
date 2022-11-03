// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ConsoleTestsPLC.Domain.Common;
using libplctag;
using libplctag.DataTypes;

namespace ConsoleTestsPLC.Domain.Entities;
public class MicrologixPlc : BaseAuditableEntity
{
    // create a List<int> to store the used ids of the created tags


    // make List<PlcTag> plcTags optional in the constructor

    private static int s_timeoutSeconds;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ipAddress"></param>
    /// <param name="plcTags"></param>
    public MicrologixPlc(string? name = null, string? ipAddress = null, int timeoutSeconds = 5)
    {
        // if null, generate a new name
        Name = name ?? $"Micrologix1100_{Id}";
        // if null, generate a new ipAddress
        IpAddress = ipAddress ?? $"192.168.0.{Id}";

        s_timeoutSeconds = timeoutSeconds;
    }
    public string Name { get; set; }
    public string IpAddress { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public string? Program { get; set; }

    public PlcType PlcType { get; set; }
    public Protocol Protocol { get; set; }

    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(s_timeoutSeconds);

    public DebugLevel DebugLevel { get; set; } = DebugLevel.None;

    public IList<IntPlcTag> PlcTags { get; set; } = new List<IntPlcTag>();

    /*
    private List<int> _usedIds = new List<int>();
    private int GetUnusedId()
    {
        // sort the _usedIds list in ascending order and get the last element
        // increment the last element by 1 and return it
        // if the list is empty, return 1
        var lastId = _usedIds.Count == 0 ? 1 : _usedIds.OrderBy(x => x).Last();
        _usedIds.Add(lastId + 1);
        return lastId + 1;
    }
    */
}
