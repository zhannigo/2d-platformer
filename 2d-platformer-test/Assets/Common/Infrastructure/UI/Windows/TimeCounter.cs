using Common.Infrastructure.Data;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.UI.Windows
{
  public class TimeCounter : ITimeCounter
  {
    private IPersistentProgressService _progressService;

    [Inject]
    public void Construct(IPersistentProgressService progressService) => 
      _progressService = progressService;

    public void StopGame(bool stop)
    {
      Time.timeScale = stop? 0.0f : 1.0f;
    }

    public float UpdateBestTime()
    {
      _progressService.Progress.WorldData.TimeData.Add(Time.time);
      float bestTime = _progressService.Progress.WorldData.TimeData.GetBestTime();
      return bestTime;
    }
  }
}