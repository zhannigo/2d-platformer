using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Character.Scripts;
using Common.Enemies.Scripts;
using Common.Infrastructure.Data;
using Common.Infrastructure.Services;
using Common.Infrastructure.StaticData;
using UnityEngine;

namespace Common.Infrastructure.Factory
{
  public interface IGameFactory
  {
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    Task<GameObject> CreateEnemy(MonsterType monsterType, Vector3 at, string id);
    HeroController CreateHero(string levelName);
    EnemyController CreateEnemyController();
    GameObject CreateHud();
    void SpawnTreasure(TreasureType treasureTeasureType, Vector3 treasurePosition);
  }
}