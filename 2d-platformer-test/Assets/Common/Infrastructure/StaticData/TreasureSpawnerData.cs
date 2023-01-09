using System;
using UnityEngine;

namespace Common.Infrastructure.StaticData
{
  [Serializable]
  public class TreasureSpawnerData
  {
    public TreasureType _teasureType;
    public Vector3 Position;

    public TreasureSpawnerData(TreasureType teasureType, Vector3 position)
    {
      _teasureType = teasureType;
      Position = position;
    }
  }

  public enum TreasureType
  {
    Coin = 0,
    Chest = 2,
    Key = 3
  }
}