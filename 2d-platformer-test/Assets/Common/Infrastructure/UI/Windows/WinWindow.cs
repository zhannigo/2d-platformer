using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Common.Infrastructure.UI.Windows
{
  public class WinWindow : MonoBehaviour, IWindow
  {
    [SerializeField] private Text _timer;
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
      float bestTime = _timeCounter.UpdateBestTime();
      DisplayTime(bestTime);
      _playButton.onClick.AddListener(LoadNewGame);
    }
    void DisplayTime(float timeToDisplay)
    {
      float minutes = Mathf.FloorToInt(timeToDisplay/60);
      float seconds = Mathf.FloorToInt(timeToDisplay % 60);
      _timer.text = string.Format("{0:00},{1:00}",minutes, seconds);
    }

    private void LoadNewGame()
    {
      ButtonClickSound.Play();
      SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
  }
}