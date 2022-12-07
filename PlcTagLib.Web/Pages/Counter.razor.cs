using Microsoft.AspNetCore.Components;
using PlcTagLib.Services;

namespace PlcTagLib.Web.Pages;
public partial class Counter : ComponentBase
{
    [Inject] public BitWatcher BitWatcher { get; set; } = default!;
    [Inject] public PeriodicBitToggle PeriodicBitToggle { get; set; } = default!;

    private readonly CancellationTokenSource _cts = new CancellationTokenSource();

    private Dictionary<int, bool> BitDictionary { get; set; } = new Dictionary<int, bool>();

    private int BitWatcherInterval
    {
        get => _bitWatcherInterval;
        set => SetBitWatcherInterval(value);
    }

    private int _bitWatcherInterval = 5;


    private int ToggleInterval
    {
        get => _toggleInterval;
        set => SetToggleInterval(value);
    }
    private int _toggleInterval = 10;

    protected override Task OnInitializedAsync()
    {
        Random random = new();
        for (var i = 0; i < 32; i++)
        {
            BitDictionary.Add(i, random.Next(0, 1) == 1);
        }
        return base.OnInitializedAsync();
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return Task.CompletedTask;


        PeriodicBitToggle = new PeriodicBitToggle(BitDictionary, ToggleInterval);
        PeriodicBitToggle.Start();
        BitWatcher = new()
        {
            BitDictionary = BitDictionary
        };

        foreach (var bit in BitDictionary)
        {
            BitWatcher.Start(bit.Key, BitWatcherInterval);


        }
        BitWatcher.BitToggled += BitWatcher_OnBitChanged!;

        return Task.CompletedTask;

    }
    private void BitWatcher_OnBitChanged(object sender, BitValueChangedEventArgs e)
    {
        BitDictionary[e.BitIndex] = e.BitValue;
        StateHasChanged();
    }

    public void Dispose()
    {
        BitWatcher.BitToggled -= BitWatcher_OnBitChanged!;
        _cts.Cancel();
        _cts.Dispose();
    }

    private void SetToggleInterval(int value)
    {

        if (value is < 1 or > 300)
        {
            return;
        }
        _toggleInterval = value;

        InvokeAsync(() =>
        {

            PeriodicBitToggle.Stop();
            PeriodicBitToggle = new PeriodicBitToggle(BitDictionary, ToggleInterval);
            PeriodicBitToggle.Start();
        });

    }

    private void SetBitWatcherInterval(int value)
    {
        if (value is < 1 or > 300)
            return;

        _bitWatcherInterval = value;

        InvokeAsync(() =>
        {
            BitWatcher.BitToggled -= BitWatcher_OnBitChanged!;
            BitWatcher.Stop();
            BitWatcher = new BitWatcher
            {
                BitDictionary = BitDictionary
            };
            foreach (var bit in BitDictionary)
                BitWatcher.Start(bit.Key, BitWatcherInterval);
            BitWatcher.BitToggled += BitWatcher_OnBitChanged!;
        }
        );


    }

}
