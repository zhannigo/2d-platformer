using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Common.Infrastructure.UI.Windows
{
  public class LoseWindow : MonoBehaviour, IWindow
  {
    [SerializeField] private Button _playButton;
    [SerializeField] private AudioSource ButtonClickSound;

    private ITimeCounter _timeCounter;
    
    public void Construct(ITimeCounter timeCounter)
    {
      _timeCounter = timeCounter;
      Init();
    }

    private void Init()
    {
      _timeCounter.StopGame(true);
      _playButton.onClick.AddListener(LoadNewGame);
    }

    private void LoadNewGame()
    {
      ButtonClickSound.Play();
      SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
  }
}