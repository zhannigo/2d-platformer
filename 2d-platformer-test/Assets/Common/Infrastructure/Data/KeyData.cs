using System;

namespace Common.Infrastructure.Data
{
  [Serializable]
  public class KeyData
  {
    public int CollectedKey;
    private event Action ChangedCollected;

    public void Add(int key)
    {
      CollectedKey += key;
      ChangedCollected?.Invoke();
    }

    public void Subtract(int key)
    {
      CollectedKey -= key;
      ChangedCollected?.Invoke();
    }
  }
}