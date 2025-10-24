// IInventory.cs
namespace Fragments.Runtime
{
    public interface IInventory
    {
        bool TryAdd(string resourceId, int amount);
    }
}
