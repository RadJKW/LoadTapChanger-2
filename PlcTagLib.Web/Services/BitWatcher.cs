namespace PlcTagLib.Web.Services;
using PlcTagLib.Web.Models;
public class BitWatcher : IDisposable
{
    private readonly BitArray _bitArray = default!;
    private readonly int _bitIndex;
    private readonly CancellationTokenSource _cts = default!;

    public BitArray BitArray => _bitArray;
    public int BitIndex => _bitIndex;
    public bool BitValue
    {
        get => _bitArray[_bitIndex];
        set => _bitArray[_bitIndex] = value;
    }

    // Add an event that is raised when the bit that the BitWatcher is monitoring changes
    public event Action<int, bool>? BitChanged;

    public BitWatcher()
    {

    }

    public BitWatcher(BitArray bitArray, int bitIndex)
    {
        _bitArray = bitArray;
        _bitIndex = bitIndex;
        _cts = new CancellationTokenSource();

        // Subscribe to the BitArray_NotifyOnBitChanged event of the BitArray
        _bitArray.NotifyOnBitChanged += BitArray_NotifyOnBitChanged;
    }

    private void BitArray_NotifyOnBitChanged(int index, bool value)
    {
        // If the bit that was changed is the bit that the BitWatcher is monitoring
        if (_bitIndex == index)
        {
            // Raise the BitChanged event
            BitChanged?.Invoke(_bitIndex, value);
        }
    }

    public void Toggle()
    {
        BitValue = !BitValue;
    }

    public void Dispose()
    {
        _cts.Cancel();
        GC.SuppressFinalize(this);
    }
}
