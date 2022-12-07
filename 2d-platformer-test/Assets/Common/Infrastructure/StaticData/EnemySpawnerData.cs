using System;
using UnityEngine;

namespace Common.Enemy.Scripts
{
  [Serializable]
  public class EnemySpawnerData
  {
    public readonly MonsterType _monsterType;
    public readonly Vector2 Position;

    public EnemySpawnerData(MonsterType monsterType, Vector2 position)
    {
      _monsterType = monsterType;
      Position = position;
    }
  }
}