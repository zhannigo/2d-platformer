using System;

namespace Common.Infrastructure.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public WorldData WorldData;

    public PlayerProgress()
    {
      WorldData = new WorldData();
    }
  }
}