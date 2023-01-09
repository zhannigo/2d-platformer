using Common.Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.Data
{
  public class SaveLoadService : ISaveLoadService
  {
    private IPersistentProgressService _progressService;
    private IGameFactory _gameFactory;
    private const string ProgressKey = "Progress";

    [Inject]
    public void Construct (IGameFactory gameFactory, IPersistentProgressService progressService)
    {
      _gameFactory = gameFactory;
      _progressService = progressService;
    }

    public void SaveProgress()
    {
      foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
      {
        progressWriter.UpdateProgress(_progressService.Progress);
      }
      PlayerPrefs.SetString(ProgressKey,_progressService.Progress.ToJson());
    }
    public PlayerProgress LoadProgress() => 
      PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
  }
}