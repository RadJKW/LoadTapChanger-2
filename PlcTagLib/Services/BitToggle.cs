using System.ComponentModel;

namespace PlcTagLib.Services;
public class PeriodicBitToggle : BackgroundWorker
{

    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private PeriodicTimer Timer { get; set; } = default!;
    private Dictionary<int, bool> BitsDictionary { get; set; } = default!;
    private readonly Random _random = default!;
    private Task _timerTask = default!;
    private readonly List<int> _bitsToggleQueue = new();
    private int _counterForRandomToggle;
    private int _toggleInterval;
    private int _counterModulus = 20;


    public PeriodicBitToggle()
    {

    }

    public PeriodicBitToggle(Dictionary<int, bool> bits, int toggleInterval)
    {
        BitsDictionary = bits;
        _random = new Random();
        _toggleInterval = toggleInterval;
        Timer = new PeriodicTimer(TimeSpan.FromMilliseconds(toggleInterval));

    }

    public void Start()
    {

        // scale the _counterModulus based on on the toggle interval
        // the lower the toggle interval, the higher the _counterModulus
        // the higher the toggle interval, the lower the _counterModulus


        if (_toggleInterval < 25)
            _counterModulus = 40;
        else if (_toggleInterval < 50)
            _counterModulus = 20;
        else if (_toggleInterval < 100)
            _counterModulus = 10;
        else if (_toggleInterval < 150)
            _counterModulus = 6;
        else
            _counterModulus = 1;

        _timerTask = StartAsync();
    }

    private async Task StartAsync()
    {
        try
        {
            while (await Timer.WaitForNextTickAsync(_cancellationTokenSource.Token))
            {
                _counterForRandomToggle++;

                // if there are bits to toggle in the queue then process them
                if (_bitsToggleQueue.Count > 0)
                {
                    foreach (var bit in _bitsToggleQueue)
                    {
                        BitsDictionary[bit] = !BitsDictionary[bit];
                    }
                    _bitsToggleQueue.Clear();

                    //Console.Write($"Queued bits toggled: {string.Join( "|", _bitsToggleQueue)}");
                }

                // This statement will block the random bits from being toggled until a condition is met. 
                // if the toggle interval is really short, then the bits shoudl be toggled less frequently
                // if the toggle interval is really long, then the bits should be toggled more frequently

                if (_counterForRandomToggle % _counterModulus != 0)
                    continue;

                _counterForRandomToggle = 0;
                var randomBit = _random.Next(0, BitsDictionary.Count);
                BitsDictionary[randomBit] = !BitsDictionary[randomBit];
                //Console.WriteLine($"Random bit toggled: {randomBit}");
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
        _cancellationTokenSource.Cancel();
        _timerTask = Task.CompletedTask;


    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _cancellationTokenSource.Dispose();
            _timerTask.Dispose();
            Timer.Dispose();
        }
        base.Dispose(disposing);
    }

    public void ToggleBit(int index)
    {
        _bitsToggleQueue.Add(index);
    }
}
