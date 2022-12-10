using System.Threading.Tasks;
using Common.Character.Scripts;
using Common.Enemies.Scripts;
using Common.Infrastructure.Services;
using Common.Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private IStaticDataService _staticData;
    private DiContainer _container;
    private EnemyController.Factory _enemyControllerFactory;

    [Inject]
    public void Construct(IStaticDataService staticDataService,
    EnemyController.Factory enemyControllerFactory, DiContainer container)
    {
      _staticData = staticDataService;
      _enemyControllerFactory = enemyControllerFactory;
      _container = container;
    }
    
    public HeroController CreateHero(string levelName)
    {
      LevelData levelData = _staticData.ForLevel(levelName);
      HeroData heroData = _staticData.ForHero();
      var controller = _container.InstantiatePrefab(heroData.Prefab, levelData.StartPoint, Quaternion.identity, null);
      return controller.GetComponent<HeroController>();
    }

    public async Task<GameObject> CreateEnemy(MonsterType monsterType, Vector3 at, string id)
    {
      EnemyData enemyData = await _staticData.ForEnemy(monsterType);
      var enemy = Object.Instantiate(enemyData.Prefab, at, Quaternion.identity);
      enemy.GetComponent<Enemy>().Id = id;
      return enemy;
    }

    public EnemyController CreateEnemyController() => 
      _enemyControllerFactory.Create();
  }
}