namespace Common.Infrastructure.UI.Windows
{
  public interface ITimeCounter
  {
    void StopGame(bool stop);
    float UpdateBestTime();
  }
}