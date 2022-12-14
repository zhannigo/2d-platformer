using Common.Infrastructure.UI.Windows;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.Services
{
  public class WindowsService : MonoBehaviour
  {
    [SerializeField] private FinishWindow _finishWindow;
    [SerializeField] private StartWindow _startWindow;
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
      _units.GameIsOver += OpenFinishWindow;
      _startWindow.Construct(_units, _timeCunter);
    }

    private void OpenFinishWindow() => 
      _finishWindow.OpenWindow();
  }
}