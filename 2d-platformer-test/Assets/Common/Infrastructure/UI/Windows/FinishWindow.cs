using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;
using Zenject;

namespace Common.Infrastructure.UI.Windows
{
  public class FinishWindow : MonoBehaviour
  {
    [SerializeField] private Text _timer;
    [SerializeField] private Button _playButton;

    private ITimeCounter _timeCounter;

    [Inject]
    public void Construct(ITimeCounter timeCounter) => 
      _timeCounter = timeCounter;

    public void OpenWindow()
    {
      gameObject.SetActive(true);
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

    private void LoadNewGame() => 
      SceneManager.LoadSceneAsync(1);
  }
}