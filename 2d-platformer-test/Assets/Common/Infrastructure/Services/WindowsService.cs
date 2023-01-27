using System;
using Common.Infrastructure.UI.Windows;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.Services
{
  public class WindowsService : MonoBehaviour
  {
    [SerializeField] private StartWindow _startWindow;
    [SerializeField] private GameObject _pauseWindow;
    [SerializeField] private GameObject _finishWindow;
    [SerializeField] private GameObject _loseWindow;
    private UnitService _units;
    private ITimeCounter _timeCunter;

    [Inject]
    public void Construct(UnitService unitService, ITimeCounter timeCounter)
    {
      _units = unitService;
      _timeCunter = timeCounter;
    }

    private void Start()
    {
      //_units.GameIsOver += OpenFinishWindow;
      _units.isWin += OpenWinWindow;
      _units.isLose += OpenLoseWindow;
      CreateWindow(WindowId.Start);
    }

    private void OpenLoseWindow() => 
      CreateWindow(WindowId.Lose);

    private void OpenWinWindow() => 
      CreateWindow(WindowId.Win);

    public void CreateWindow(WindowId windowId)
    {
      switch (windowId)
      {
        case WindowId.Start:
          _startWindow.Construct(_units, _timeCunter);
          break;
        case WindowId.Pause:
          Initialize(_pauseWindow);
          break;
        case WindowId.Lose:
          Initialize(_loseWindow);
          break;
        case WindowId.Win:
          Initialize(_finishWindow);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(windowId), windowId, "Unknown window");
      }
    }

    private void Initialize(GameObject windowPrefab)
    {
      GameObject window = Instantiate(windowPrefab, transform);
      window.GetComponent<IWindow>().Construct(_timeCunter);
    }
  }
}