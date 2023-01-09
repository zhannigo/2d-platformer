using System;

namespace Common.Infrastructure.Data
{
  [Serializable]
  public class LootData
  {
    public int Collected;
    public event Action ChangedCollected;

    public void Collect(Loot loot)
    {
      Collected += loot.Value;
      ChangedCollected?.Invoke();
    }

    public void Add(int loot)
    {
      Collected += loot;
      ChangedCollected?.Invoke();
    }

    public void Subtract(int loot)
    {
      Collected -= loot;
      ChangedCollected?.Invoke();
    }
  }
}