using System.ComponentModel;

namespace PlcTagLib.Services;
public class BitWatcher : BackgroundWorker
{
    private readonly CancellationTokenSource _cts = new();

    public Dictionary<int, bool> BitDictionary { get; set; } = new();
    private PeriodicTimer Timer { get; set; } = default!;

    private Task _timerTask = default!;
    private Task _stopTask = default!;
    private bool _previousValue;

    public event EventHandler<BitValueChangedEventArgs> BitToggled = delegate {};
    private void OnBitToggled(int key, bool bit)
    {
        BitToggled?.Invoke(this, new BitValueChangedEventArgs()
        {
            BitIndex = key, BitValue = bit
        });
    }

    public void Start(int bitIndexKey, int millisecondInterval)
    {
        Timer = new PeriodicTimer(TimeSpan.FromMilliseconds(millisecondInterval));
        _timerTask = StartAsync(Timer, bitIndexKey);
    }


    private async Task StartAsync(PeriodicTimer timer, int bitIndexKey)
    {
        try
        {
            //Console.WriteLine($"Starting bit watcher for bit {bitIndexKey}");
            while (await timer.WaitForNextTickAsync(_cts.Token))
            {
                var currentValue = BitDictionary![bitIndexKey];
                if (currentValue == _previousValue)
                    continue;

                OnBitToggled(bitIndexKey, currentValue);
                _previousValue = currentValue;
            }
            Stop();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public void Stop()
    {
        _stopTask = StopAsync();
    }

    private async Task StopAsync()
    {
        _cts.Cancel();
        await _timerTask;
        Timer.Dispose();
        _cts.Dispose();
    }


    protected override void Dispose(bool disposing)
    {

        if (disposing)
        {
            Stop();
            _cts.Cancel();
            _cts.Dispose();
            _timerTask.Dispose();
        }
        base.Dispose(disposing);
    }
}

public class BitValueChangedEventArgs : EventArgs
{
    public int BitIndex { get; init; }
    public bool BitValue { get; init; }
}
