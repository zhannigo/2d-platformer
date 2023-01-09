using Common.Infrastructure.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.Infrastructure.UI.Elements
{
  public class LootCounter : MonoBehaviour
  {
    public Text _textCounter;
    private IPersistentProgressService _progressService;

    [Inject]
    public void Construct(IPersistentProgressService progressService)
    {
      _progressService = progressService;
      _progressService.Progress.WorldData.LootData.ChangedCollected += UpdateCounter;
    }
    private void Start() => 
      UpdateCounter();

    private void UpdateCounter() => 
      _textCounter.text = $"{_progressService.Progress.WorldData.LootData.Collected}";
  }
}