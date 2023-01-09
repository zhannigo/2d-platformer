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
    private string TreasuresDataPath = "StaticData/World/Treasure";
    private Dictionary<string, LevelStaticData> _levels = new Dictionary<string, LevelStaticData>();
    private Dictionary<MonsterType, EnemyStaticData> _enemies = new Dictionary<MonsterType, EnemyStaticData>();
    private Dictionary<TreasureType,TreasureStaticData> _treasures = new Dictionary<TreasureType, TreasureStaticData>();
    private HeroStaticData _hero;

    public void LoadStaticData()
    {
      _levels = Resources.LoadAll<LevelStaticData>(LevelDataPath)
        .ToDictionary(x => x.LevelKey, x => x);
      _enemies = Resources.LoadAll<EnemyStaticData>(EnemyDataPath)
        .ToDictionary(x => x._monsterType, x => x);
      _treasures = Resources.LoadAll<TreasureStaticData>(TreasuresDataPath)
        .ToDictionary(x => x._treasureType, x => x);
      _hero = Resources.Load<HeroStaticData>(HeroDataPath);
    }

    public LevelStaticData ForLevel(string levelName)
    {
      return _levels.TryGetValue(levelName, out LevelStaticData levelData)
        ? levelData
        : null;
    }

    public Task<EnemyStaticData> ForEnemy(MonsterType enemyType)
    {
      return Task.FromResult(_enemies.TryGetValue(enemyType, out EnemyStaticData staticData)
        ? staticData
        : null);
    }

    public Task<TreasureStaticData> ForTreasures(TreasureType treasureType)
    {
      return Task.FromResult(_treasures.TryGetValue(treasureType, out TreasureStaticData staticData)
        ? staticData
        : null);
    }
    public HeroStaticData ForHero() => 
      _hero;
  }
}