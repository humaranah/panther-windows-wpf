namespace Panther.Core.Models
{
    public interface IIdentificable
    {
        long Id { get; set; }
        string Name { get; set; }
    }
}
