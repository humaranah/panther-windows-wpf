namespace Panther.Core.Player
{
    public interface IAudioInformationReader<TInformation>
    {
        TInformation Information { get; }
    }
}
