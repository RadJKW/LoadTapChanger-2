namespace PlcTagLib.Services;
public interface IBitCollectionService
{
    Task<int> GetBitValueAsync(int index);
    Task SetBitValueAsync(int index, int value);
    public void StartBitToggler(int interval);
    void StopBitToggler();
}

