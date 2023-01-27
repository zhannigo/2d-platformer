using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Common.Infrastructure.UI.Windows
{
  public class PauseWindow : MonoBehaviour, IWindow
  {
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private AudioSource ButtonClickSound;
    private ITimeCounter _timeCounter;
    private bool _isPaused = true;

    public void Construct(ITimeCounter timeCunter) => 
      _timeCounter = timeCunter;

    private void Start()
    {
      _timeCounter.StopGame(_isPaused);
      _isPaused = !_isPaused;
      _continueButton.onClick.AddListener(ContinueGame);
      _playButton.onClick.AddListener(PlayNewGame);
    }

    private void PlayNewGame()
    {
      ButtonClickSound.Play();
      SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    private void ContinueGame()
    {
      ButtonClickSound.Play();
      _timeCounter.StopGame(_isPaused);
      Destroy(this.gameObject);
    }
  }
}