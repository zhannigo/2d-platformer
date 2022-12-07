using System.Collections.Generic;
using UnityEngine;

namespace Common.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/LevelData")]
  public class LevelData : ScriptableObject
  {
    public string LevelKey;
    public List<EnemySpawnerData> Spawners;
  }
}