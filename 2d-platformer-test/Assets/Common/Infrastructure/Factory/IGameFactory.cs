using System.Threading.Tasks;
using Common.Character.Scripts;
using Common.Enemies.Scripts;
using UnityEngine;

namespace Common.Infrastructure.Factory
{
  public interface IGameFactory
  {
    Task<GameObject> CreateEnemy(MonsterType monsterType, Vector3 at, string id);
    HeroController CreateHero(string levelName);
    EnemyController CreateEnemyController();
  }
}