using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Character.Scripts;
using Common.Enemies.Scripts;
using Common.Infrastructure.Data;
using Common.Infrastructure.Services;
using Common.Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
    
    private IStaticDataService _staticData;
    private DiContainer _container;
    private string _HudPath = "HUD/HUD";

    [Inject]
    public void Construct(IStaticDataService staticDataService, DiContainer container)
    {
      _staticData = staticDataService;
      _container = container;
    }
    
    public HeroController CreateHero(string levelName)
    {
      LevelStaticData levelData = _staticData.ForLevel(levelName);
      HeroStaticData heroData = _staticData.ForHero();
      var controllerObject = _container.InstantiatePrefab(heroData.Prefab, levelData.StartPoint, Quaternion.identity, null);
      HeroController controller = controllerObject.GetComponent<HeroController>();
      return controller;
    }

    public async Task<GameObject> CreateEnemy(MonsterType monsterType, Vector3 at, string id)
    {
      EnemyStaticData enemyData = await _staticData.ForEnemy(monsterType);
      var enemy = Object.Instantiate(enemyData.Prefab, at, Quaternion.identity);
      enemy.GetComponent<Enemy>().Id = id;
      return enemy;
    }

    public EnemyController CreateEnemyController()
    {
      EnemiesData data = new EnemiesData();
      EnemyController enemyController = _container.Instantiate<EnemyController>();
      enemyController.Construct(data);
      return enemyController;
    }

    public GameObject CreateHud() => 
      _container.InstantiatePrefabResource(_HudPath);

    public async void SpawnTreasure(TreasureType treasureType, Vector3 at)
    {
      TreasureStaticData treasureData = await _staticData.ForTreasures(treasureType);
      _container.InstantiatePrefab(treasureData.Prefab, at, Quaternion.identity, null);
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>()) {
        Register(progressReader);
      }
    }

    private void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter)
      {
        ProgressWriters.Add(progressWriter);
      }
      ProgressReaders.Add(progressReader);
    }
  }
}