using System.Threading.Tasks;
using Common.Enemies.Scripts;
using Common.Infrastructure.StaticData;

namespace Common.Infrastructure.Services
{
  public interface IStaticDataService
  {
    void LoadStaticData();
    LevelData ForLevel(string levelName);
    Task<EnemyData> ForEnemy(MonsterType monsterType);
    HeroData ForHero();
  }
}