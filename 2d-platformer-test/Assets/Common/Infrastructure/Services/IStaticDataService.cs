using System.Threading.Tasks;
using Common.Enemy.Scripts;

namespace Common.Infrastructure.StaticData
{
  public interface IStaticDataService
  {
    void LoadStaticData();
    LevelData ForLevel(string levelName);
    Task<EnemyData> ForEnemy(MonsterType monsterType);
    HeroData ForHero();
  }
}