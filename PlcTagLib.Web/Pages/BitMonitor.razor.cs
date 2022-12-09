using Microsoft.AspNetCore.Components;
using PlcTagLib.Web.Models;
using PlcTagLib.Web.Services;

namespace PlcTagLib.Web.Pages;

public partial class BitMonitor : ComponentBase
{
    [Inject] private BitToggle BitToggleService { get; set; } = default!;
    [Inject] private BitWatcher BitWatcher { get; set; } = default!;

    private IEnumerable<BitWatcher> BitWatchers { get; set; } = default!;

    // Add a property to control the Delay of the BitToggle service
    private int Delay { get; set; } = 1000;

    private BitArray BitArray { get; set; } = new BitArray(8);

    // This BitArray length will get the length of BitArray and set the length to the new BitArray
    // When the length is updated, multiple things should happen
    // - The BitArray should be resized to the new length
    // - BitToggleService should be updated with the new length
    // if the Array is resized to be smaller
    // - BitWatchers assigned to the removed bits should be disposed. 
    // if the array is resized to be larger
    // - BitWatchers should be added for the new bits
    private int BitArrayLength
    {
        get => BitArray.Length;
        set => BitArrayChanged(new BitArray(value));

    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        BitToggleService.BitArray = BitArray;
        BitToggleService.Delay = Delay;
        BitToggleService.BitArrayChanged += BitArrayChanged;

        BitWatchers = Enumerable.Range(0, BitArray.Length)
            .Select(i =>
            {
                // Create a new BitWatcher instance for each bit in the BitArray
                var bitWatcher = new BitWatcher(BitArray, i);

                // Subscribe to the BitChanged event of the BitWatcher
                bitWatcher.BitChanged += NotifyOnBitChanged;

                return bitWatcher;
            });

        BitToggleService.Start();

        // subscribe to the NotifyOnBitChanged event of the BitArray
        // this will be called when a bit is changed
        // and the BitArray will be updated with the new values of the bits
        BitArray.NotifyOnBitChanged += (index, value) =>
        {
            BitArray[index] = value;
            StateHasChanged();
        };

    }


    /*protected override void OnAfterRender(bool firstRender)
    {

        if (firstRender)
        {
            BitToggleService.Start();
        }


    }*/

    private void BitArrayChanged(BitArray bitArray)
    {
        // check for null 
        if (bitArray is null)
            return;
        // Create a new BitArray instance with the same length as the BitArray in the BitToggle service
        var newBitArray = new BitArray(bitArray.Length);

        // Update the BitArray in the BitMonitor page
        BitArray = newBitArray;

        // Update the BitArray in the BitToggle service
        BitToggleService.BitArray = BitArray;
    }

    private void NotifyOnBitChanged(int index, bool value)
    {
        // Update the BitArray in the BitMonitor page
        BitArray[index] = value;

        // Update the BitArray in the BitToggle service
        BitToggleService.BitArray[index] = value;

        // Update the BitWatchers list in the BitMonitor page
        BitWatchers = BitWatchers.Select(w =>
        {
            if (w.BitIndex == index)
            {
                w.BitValue = value;
            }

            return w;
        });
    }

    private void SetBitArrayLength(int newValue)
    {
        BitArrayLength = newValue;
    }


    protected override void OnParametersSet()
    {
        // Update the Delay of the BitToggle service
        BitToggleService.Delay = Delay;
    }

    public void Dispose()
    {

        // Dispose of the BitToggle service
        BitToggleService.Dispose();
        BitWatchers.ToList().ForEach(w => w.Dispose());
    }


}
