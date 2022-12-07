using System.Threading.Tasks;
using Common.Character.Scripts;
using Common.Enemy.Scripts;
using UnityEngine;

namespace Common.Infrastructure
{
  public interface IGameFactory
  {
    Task<GameObject> CreateEnemy(MonsterType monsterType, Vector3 at, string id);
    HeroController CreateHero(string levelName);
    EnemyController CreateEnemyController();
  }
}