using System.Collections.Generic;
using UnityEngine;

namespace Common.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/LevelData")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public List<EnemySpawnerData> Spawners;
    public List<TreasureSpawnerData> Treasure;
    public Vector3 StartPoint;
  }
}