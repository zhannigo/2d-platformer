using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Common.Infrastructure.UI.Windows
{
  public class FinishWindow : MonoBehaviour
  {
    [SerializeField] private Text _timer;
    [SerializeField] private Button _playButton;

    public void OpenWindow()
    {
      gameObject.SetActive(true);
      DisplayTime(Time.time);
      _playButton.onClick.AddListener(LoadNewGame);
    }
    void DisplayTime(float timeToDisplay)
    {
      float minutes = Mathf.FloorToInt(timeToDisplay/60);
      float seconds = Mathf.FloorToInt(timeToDisplay % 60);
      _timer.text = string.Format("{0:00},{1:00}",minutes, seconds);
    }

    public void LoadNewGame() => 
      SceneManager.LoadSceneAsync(1);
  }
}