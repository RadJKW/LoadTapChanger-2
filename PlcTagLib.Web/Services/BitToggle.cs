namespace PlcTagLib.Web.Services;
using PlcTagLib.Web.Models;
// BitToggle.cs
public class BitToggle : IDisposable
{
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();

    public int Delay { get; set; } = 1000;

    public BitArray BitArray { get; set; } = default!;

    public Action<BitArray> BitArrayChanged { get; set; } = default!;

    public void Start()
    {
        Task.Run(async () =>
        {
            // Set the initial length of the BitArray in the BitToggle service
            BitArrayChanged.Invoke(BitArray);

            while (!_cts.IsCancellationRequested)
            {
                // Select a random bit in the BitArray
                var bitIndex = new Random().Next(0, BitArray.Length);

                // Toggle the selected bit
                BitArray[bitIndex] = !BitArray[bitIndex];

                // Wait for the specified delay before toggling the next bit
                await Task.Delay(Delay, _cts.Token);
            }
        });
    }

    public void Dispose()
    {
        _cts.Cancel();
        GC.SuppressFinalize(this);
    }
}


