using Common.Enemies.Scripts;
using UnityEngine;

namespace Common.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "StaticData/EnemyData")]
  public class EnemyStaticData : ScriptableObject
  {
    public MonsterType _monsterType;
    public GameObject Prefab;
  }
}