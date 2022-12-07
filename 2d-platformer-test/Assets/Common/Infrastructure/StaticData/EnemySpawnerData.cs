using System;
using Common.Enemy.Scripts;
using UnityEngine;

namespace Common.Infrastructure.StaticData
{
  [Serializable]
  public class EnemySpawnerData
  {
    public string _id;
    public MonsterType _monsterType;
    public Vector3 Position;

    public EnemySpawnerData(string id, MonsterType monsterType, Vector3 position)
    {
      _id = id;
      _monsterType = monsterType;
      Position = position;
    }
  }
}