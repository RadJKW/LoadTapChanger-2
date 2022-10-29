// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Services;
using MudBlazorTest.Server.Services.Base;
using static MudBlazor.CategoryTypes;

namespace MudBlazorTest.Server.Pages.PLC;

public partial class PlcTable
{

    private class LocalPlc : DetailsPlcDto
    {
        public bool ShowDetails { get; set; }

    }
    private MudTable<LocalPlc>? _plcTable;
    private List<DetailsPlcDto> _dtoPlcList = new();
    private List<LocalPlc> _localPlcList = new();
    private HashSet<LocalPlc> _selectedItems = new();


    private bool _loading = false;
    private bool _ronly = false;
    private string _searchString = "";



    // create a new class that inherits from detailsPlcDto
    // add a new property to the class => private bool _showDetails = false;


    protected override async Task OnInitializedAsync()
    {
        _localPlcList.Clear();
        _loading = true;
        await Task.Delay(2000);
        var response = await plcService.Get();
        if (response.Success)
        {
            _dtoPlcList = response.Data!;
            _localPlcList = Map<DetailsPlcDto, LocalPlc>(_dtoPlcList);
        }


        _loading = false;

    }

    private int selectedRowNumber = -1;
    private List<string> clickedEvents = new();
    private List<ReadPlcTagDto> _plcTagList = new();
    private bool _showTags = false;

    private void RowClickEvent(TableRowClickEventArgs<LocalPlc> tableRowClickEventArgs)
    {
        clickedEvents.Add("Row has been clicked");

    }

    // TODO: ISSUE: First click on row does not render cards.... 
    private string SelectedRowClassFunc(LocalPlc selectedPlc, int rowNumber)
    {
        if (selectedRowNumber == rowNumber)
        {
            selectedRowNumber = -1;
            clickedEvents.Add("Selected Row: None");

            return string.Empty;
        }
        else if (_plcTable!.SelectedItem != null && _plcTable.SelectedItem.Equals(selectedPlc))
        {
            selectedRowNumber = rowNumber;
            clickedEvents.Add($"Selected Row: {rowNumber}");
            _plcTagList = (List<ReadPlcTagDto>)selectedPlc.PlcTags;
            StateHasChanged(); // this works but the return value is not reached.
            return "selected";
        }
        else
        {
            return string.Empty;
        }


    }





    /// <summary>
    /// Custom Generic Mapper. 
    /// Should probably just use an Nuget Package
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static List<TDestination> Map<TSource, TDestination>(List<TSource> source)
    {
        List<TDestination> destination = new();
        foreach (TSource sourceItem in source)
        {
            TDestination destinationItem = Activator.CreateInstance<TDestination>();
            foreach (var sourceProperty in sourceItem!.GetType().GetProperties())
            {
                var destinationProperty = destinationItem!.GetType()
                    .GetProperty(sourceProperty.Name);
                if (destinationProperty != null)
                {
                    destinationProperty.SetValue(destinationItem, sourceProperty.GetValue(sourceItem));
                }
            }
            destination.Add(destinationItem);
        }
        return destination;

    }



}

#if false // do not uncomment. 
#region Useful Code Snippets
 
#region Dynamic Table Height
 <MudTable Height="@(_dtoPlcList.Count <= 4 ? "150px": "300px" )>"
#endregion

#region BreakpointService
#region MudBlazor Component
    <MudCard Class="pa-5">
        <MudText>Size started with @_start</MudText>
        @if (_breakpointHistory.Count > 0)
        {
            <MudText>And continued with: </MudText>
            <MudList Dense="_breakpointHistory.Count > 10">
                @foreach (var item in _breakpointHistory)
                {
                    <MudListItem Text="@item.ToString()"></MudListItem>
                }
            </MudList>
        }
    </MudCard>
#endregion
#region CodeBehind
    [inject]
    ibreakpointservice? breakpointlistener { get; set; }
    private list<breakpoint> _breakpointhistory = new();
    private guid _subscriptionid;
    private breakpoint _start;
    protected override async task onafterrenderasync(bool firstrender)
    {
        if (firstrender)
        {
            var subscriptionresult = await breakpointlistener!.subscribe((breakpoint) =>
            {
                _breakpointhistory.add(breakpoint);
                invokeasync(statehaschanged);
            }, new resizeoptions
            {
                reportrate = 250,
                notifyonbreakpointonly = true,
            });

            _start = subscriptionresult.breakpoint;
            _subscriptionid = subscriptionresult.subscriptionid;
            statehaschanged();
        }

        await base.onafterrenderasync(firstrender);
    }
    public async ValueTask DisposeAsync() => await BreakpointListener!.Unsubscribe(_subscriptionId);
#endregion
#endregion

#endregion
#endif
