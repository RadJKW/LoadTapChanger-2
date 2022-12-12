namespace PlcTagLib.Services;
using Microsoft.Extensions.Logging;
public class BitCollection : IBitCollectionService
{
    private readonly ILogger<BitCollection> _logger;
    private int[] _bits;
    private readonly Random _random;
    private CancellationTokenSource _cancellationTokenSource;
    private Task _bitTogglerTask;
    
        // Other class members and methods here...

        // Remove the size parameter from the constructor
        public BitCollection(ILogger<BitCollection> logger)
        {
            _random = new Random();
            _logger = logger;

            // Set the default size of the bit array
            ArraySize = 8;
        }

        // Add a property for the size of the bit array, with a set accessor
        // that uses the Array.Resize method to change the size of the _bits field
        public int ArraySize
        {
            get { return _bits.Length; }
            set
            {
                // Use the Array.Resize method to change the size of the _bits field
                Array.Resize(ref _bits, value);
            }
        }
        public int[] Bits
        {
            get { return _bits; }
            set { _bits = value; }
        }

        // Other class members and methods here...
    


    public Task<int> GetBitValueAsync(int index)
    {
        _logger.LogInformation("Getting value of bit at index {Index}", index);
        return Task.FromResult(_bits[index]);
    }

    public Task SetBitValueAsync(int index, int value)
    {
        _logger.LogInformation("Setting value of bit at index {Index} to {Value}", index, value);
        _bits[index] = value;
        return Task.CompletedTask;
    }

    public void StartBitToggler(int interval)
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _bitTogglerTask = Task.Run(() => BitToggler(interval, _cancellationTokenSource.Token), _cancellationTokenSource.Token);
    }

    public void StopBitToggler()
    {
        _cancellationTokenSource.Cancel();
    }

    private async Task BitToggler(int interval, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            // Wait for the specified interval
            await Task.Delay(interval, cancellationToken); 
            // Choose a random bit and toggle its value
            var index = _random.Next(_bits.Length);
            _bits[index] = _bits[index] == 1 ? 0 : 1;

            // Log the value change
            _logger.LogInformation("Bit at index {Index} was toggled to {Value}", index, _bits[index]);

            // Raise an event or call a delegate to notify subscribers
            // that the value of a bit has changed
        }
    }
}

