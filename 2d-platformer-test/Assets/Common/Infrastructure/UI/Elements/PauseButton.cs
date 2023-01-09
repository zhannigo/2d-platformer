using Common.Infrastructure.UI.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.Infrastructure.UI.Elements
{
  public class PauseButton : MonoBehaviour
  {
    public Button _button;
    private bool _isPaused = true;
    private ITimeCounter _timeCounter;

    [Inject]
    public void Construct(ITimeCounter timeCounter) => 
      _timeCounter = timeCounter;

    private void Start() => 
      _button.onClick.AddListener(PauseGame);

    private void PauseGame()
    {
      _timeCounter.StopGame(_isPaused);
      _isPaused = !_isPaused;
    }
  }
}