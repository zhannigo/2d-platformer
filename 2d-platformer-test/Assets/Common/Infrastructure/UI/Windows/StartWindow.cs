using Common.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.Infrastructure.UI.Windows
{
  public class StartWindow : MonoBehaviour
  {
    [SerializeField] private Button _playButton;
    private ITimeCounter _timeCounter;
    private UnitService _units;

    public void Construct(UnitService unitService, ITimeCounter timeCounter)
    {
      _units = unitService;
      _timeCounter = timeCounter;
    }

    private void Start()
    {
      _timeCounter.StopGame(true);
      _playButton.onClick.AddListener(CloseWindow);
    }

    private void CloseWindow()
    {
      _timeCounter.StopGame(false);
      _units.EnemyController.Initialize();
      gameObject.SetActive(false);
    }
  }
}