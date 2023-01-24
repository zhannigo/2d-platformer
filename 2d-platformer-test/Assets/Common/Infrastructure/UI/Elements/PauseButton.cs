using Common.Infrastructure.UI.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.Infrastructure.UI.Elements
{
  public class PauseButton : MonoBehaviour
  {
    [SerializeField] private Button _button;
    [SerializeField] private AudioSource ButtonClickSound;
    private bool _isPaused = true;
    private ITimeCounter _timeCounter;

    [Inject]
    public void Construct(ITimeCounter timeCounter) => 
      _timeCounter = timeCounter;

    private void Start() => 
      _button.onClick.AddListener(PauseGame);

    private void PauseGame()
    {
      ButtonClickSound.Play();
      _timeCounter.StopGame(_isPaused);
      _isPaused = !_isPaused;
    }
  }
}