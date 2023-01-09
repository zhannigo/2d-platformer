using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Character.Scripts;
using Common.Enemies.Scripts;
using Common.Infrastructure.Data;
using Common.Infrastructure.StaticData;
using Common.Infrastructure.Factory;
using Common.Infrastructure.Services;
using Common.Infrastructure.UI.Elements;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Common.Infrastructure
{ 
  public class LoadLevelService 
  {
    private string levelName = "Main";
    private List<EnemySpawnerData> _spawners;

    private IRandomService _random;
    private IStaticDataService _staticData;
    private IGameFactory _factory;
    private UnitService _units;
    private static IPersistentProgressService _progressService;
    private static ISaveLoadService _saveLoadService;

    [Inject]
    void Construct(IStaticDataService staticData, 
      IRandomService randomService, IGameFactory factory, UnitService unitService,
      IPersistentProgressService progressService, ISaveLoadService saveLoadService)
    {
      _staticData = staticData;
      _random = randomService;
      _factory = factory;
      _units = unitService;
      _progressService = progressService;
      _saveLoadService = saveLoadService;
      LoadLevel();
    }

    private void LoadLevel()
    {
      _staticData.LoadStaticData();
      LoadOrCreateNewProgress();
      LoadTreasure();
      LoadUnits();
    }

    private static void LoadOrCreateNewProgress() => 
      _progressService.Progress = _saveLoadService.LoadProgress() ?? new PlayerProgress();

    private void LoadTreasure()
    {
      var treasures = _staticData.ForLevel(levelName).Treasure;
      foreach (TreasureSpawnerData treasure in treasures)
      {
        _factory.SpawnTreasure(treasure._teasureType, treasure.Position);
      }
    }

    private void LoadUnits()
    {
      SpawnEnemies();
      GameObject hud = _factory.CreateHud();
      SpawnHero(hud);
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
        if (!_units.EnemyController._enemies.CheckEnemy(spawnPoint._id))
        {
          SpawnEnemy(spawnPoint);
        }
      }
    }

    private void SpawnHero(GameObject hud)
    {
      var controller= _factory.CreateHero(levelName);
      _units.HeroController = controller;
      var hpBar = hud.GetComponentInChildren<HpBar>();
      controller.Init(new HeroUI(hpBar));
    }

    private async Task SpawnEnemy(EnemySpawnerData spawnPoint)
    {
      GameObject enemyPrefab = await _factory.CreateEnemy(spawnPoint._monsterType, spawnPoint.Position, spawnPoint._id);
      _units.EnemyController._enemies.AddEnemy(spawnPoint._id, enemyPrefab.GetComponent<Enemy>());
    }

    private int GetRandomSpawnIndex() => 
      _random.Next(_spawners.Count);
  }
}