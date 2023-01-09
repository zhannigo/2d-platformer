namespace Common.Infrastructure.Data
{
  public interface ISaveLoadService
  {
    void SaveProgress();
    PlayerProgress LoadProgress();
  }
}