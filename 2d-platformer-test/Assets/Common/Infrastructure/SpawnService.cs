using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Enemies.Scripts;
using Common.Infrastructure.StaticData;
using Common.Infrastructure.Factory;
using Common.Infrastructure.Services;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Common.Infrastructure
{ 
  public class SpawnService 
  {
    private string levelName = "Main";
    private List<EnemySpawnerData> _spawners;

    private IRandomService _random;
    private IStaticDataService _staticData;
    private IGameFactory _factory;
    private UnitService _units;

    [Inject]
    void Construct(IStaticDataService staticData, 
      IRandomService randomService, IGameFactory factory, UnitService unitService)
    {
      _staticData = staticData;
      _random = randomService;
      _factory = factory;
      _units = unitService;
      _staticData.LoadStaticData();
      LoadUnits();
    }

    private void LoadUnits()
    {
      SpawnEnemies();
      SpawnHero();
    }

    private void SpawnHero()
    {
      var controller= _factory.CreateHero(levelName);
      _units.HeroController = controller;
    }

    private void SpawnEnemies()
    {
      _spawners = _staticData.ForLevel(levelName).Spawners;
      if (_spawners.Count == 0)
      {
        return;
      }

      _units.EnemyController = _factory.CreateEnemyController();
      int enemyCount = Random.Range(1, _spawners.Count);
      for (int i = 0; i < enemyCount; i++)
      {
        EnemySpawnerData spawnPoint = _spawners[GetRandomSpawnIndex()];
        if (!_units.EnemyController._enemies.ContainsKey(spawnPoint._id))
        {
          SpawnEnemy(spawnPoint);
        }
      }
    }

    private async Task SpawnEnemy(EnemySpawnerData spawnPoint)
    {
      GameObject enemyPrefab = await _factory.CreateEnemy(spawnPoint._monsterType, spawnPoint.Position, spawnPoint._id);
      _units.EnemyController.AddEnemy(spawnPoint._id, enemyPrefab.GetComponent<Enemy>());
    }

    private int GetRandomSpawnIndex() => 
      _random.Next(_spawners.Count);
  }
}