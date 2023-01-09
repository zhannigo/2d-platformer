using System;

namespace Common.Infrastructure.Data
{
  [Serializable]
  public class WorldData
  {
    public LootData LootData;
    public TimeData TimeData;

    public WorldData()
    {
      LootData = new LootData();
      TimeData = new TimeData();
    }
  }
}