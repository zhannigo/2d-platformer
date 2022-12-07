using System.Collections.Generic;
using Common.Infrastructure.StaticData;
using Common.Enemy.Scripts;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Common.Infrastructure
{ 
  public class SpawnService : MonoBehaviour
  {
    private string levelName = "Main";
    private List<EnemySpawnerData> _spawners;

    private IRandomService _random;
    private IStaticDataService _staticData;
    private IGameFactory _factory;
    private EnemyController _enemyController;

    [Inject]
    void Construct( IStaticDataService staticData, IRandomService randomService, IGameFactory factory, EnemyController enemyController)
    {
      _staticData = staticData;
      _random = randomService;
      _factory = factory;
      _enemyController = enemyController;
      SpawnEnemies();
    }
    
    private async void SpawnEnemies()
    {
      _staticData.LoadStaticData();
      _spawners = _staticData.ForLevel(levelName).Spawners;

      if (_spawners.Count == 0)
      {
        return;
      }
      var enemyCount = Random.Range(1, _spawners.Count);
      
      for (int i = 0; i < enemyCount; i++)
      {
        EnemySpawnerData spawnPoint = _spawners[GetRandomSpawnIndex()];
        Vector3 spawnPointPosition = spawnPoint.Position;
        GameObject enemyPrefab = await _factory.CreateEnemy(spawnPoint._monsterType, spawnPointPosition);
        _enemyController.AddEnemy(spawnPoint._id, enemyPrefab.GetComponent<EnemyModel>());
      }
    }

    private int GetRandomSpawnIndex() => 
      _random.Next(_spawners.Count);
  }
}