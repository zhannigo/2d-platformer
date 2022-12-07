using UnityEngine;
using UnityEngine.UI;

namespace Common.Infrastructure.UI.Elements
{
  public class PauseButton : MonoBehaviour
  {
    public Button _button;
    private bool _isPaused = false;

    private void Start() => 
      _button.onClick.AddListener(PauseGame);

    private void PauseGame()
    {
      Time.timeScale = _isPaused ? 0.0f : 1.0f;
      _isPaused = !_isPaused;
    }
  }
}