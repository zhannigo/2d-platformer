using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Enemies.Scripts;
using Common.Infrastructure.StaticData;
using UnityEngine;

namespace Common.Infrastructure.Services
{
  public class StaticDataService : IStaticDataService
  {
    private string LevelDataPath = "StaticData/World";
    private string EnemyDataPath = "StaticData/Enemy";
    private string HeroDataPath = "StaticData/Hero/HeroData";
    private Dictionary<string, LevelData> _levels = new Dictionary<string, LevelData>();
    private Dictionary<MonsterType, EnemyData> _enemies = new Dictionary<MonsterType, EnemyData>();
    private HeroData _hero;

    public void LoadStaticData()
    {
      _levels = Resources.LoadAll<LevelData>(LevelDataPath)
        .ToDictionary(x => x.LevelKey, x => x);
      _enemies = Resources.LoadAll<EnemyData>(EnemyDataPath)
        .ToDictionary(x => x._monsterType, x => x);
      _hero = Resources.Load<HeroData>(HeroDataPath);
    }

    public LevelData ForLevel(string levelName)
    {
      return _levels.TryGetValue(levelName, out LevelData levelData)
        ? levelData
        : null;
    }

    public async Task<EnemyData> ForEnemy(MonsterType enemyType)
    {
      return _enemies.TryGetValue(enemyType, out EnemyData staticData)
        ? staticData
        : null;
    }

    public HeroData ForHero() => 
      _hero;
  }
}