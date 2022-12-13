using System.Threading.Tasks;
using Common.Enemies.Scripts;
using Common.Infrastructure.StaticData;

namespace Common.Infrastructure.Services
{
  public interface IStaticDataService
  {
    void LoadStaticData();
    LevelStaticData ForLevel(string levelName);
    Task<EnemyStaticData> ForEnemy(MonsterType monsterType);
    HeroStaticData ForHero();
  }
}