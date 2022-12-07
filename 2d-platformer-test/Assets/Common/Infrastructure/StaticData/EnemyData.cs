using Common.Enemy.Scripts;
using UnityEngine;

namespace Common.Character.Scripts
{
  [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/EnemyData")]
  public class EnemyData : ScriptableObject
  {
    public MonsterType _monsterType;
    public GameObject Prefab;
  }
}