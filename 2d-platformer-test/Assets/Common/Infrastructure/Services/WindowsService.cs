using System;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.Windows
{
  public class WindowsService : MonoBehaviour
  {
    [SerializeField]private FinishWindow _finishWindow;
    private UnitService _units;

    [Inject]
    public void Construct(UnitService unitService) => 
      _units = unitService;

    private void Start() => 
      _units.GameIsOver += OpenFinishWindow;

    private void OpenFinishWindow() => 
      _finishWindow.OpenWindow();
  }
}