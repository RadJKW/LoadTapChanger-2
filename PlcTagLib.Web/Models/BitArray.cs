using System.Collections;

namespace PlcTagLib.Web.Models;
public class BitArray : IEnumerable
{
    private readonly bool[] _bits;

    public int Length => _bits.Length;

    public bool this[int index]
    {
        get => _bits[index];
        set
        {
            if (_bits[index] != value)
            {
                _bits[index] = value;
                NotifyOnBitChanged?.Invoke(index, value);
            }
        }
    }

    // what does this event do 
    // - it is used to notify the BitToggle service that a bit has changed
    public event Action<int, bool>? NotifyOnBitChanged;

    public BitArray(int length)
    {
        _bits = new bool[length];
    }

    public IEnumerator GetEnumerator()
    {
        return _bits.GetEnumerator();
    }
}

