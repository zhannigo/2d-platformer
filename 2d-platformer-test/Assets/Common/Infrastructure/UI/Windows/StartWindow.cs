using UnityEngine;
using UnityEngine.UI;

namespace Common.Infrastructure.UI.Windows
{
  public class StartWindow : MonoBehaviour
  {
    [SerializeField] private Button _playButton;
    private void Start()
    {
      Time.timeScale = 0.0f;
      _playButton.onClick.AddListener(CloseWindow);
    }

    private void CloseWindow()
    {
      Time.timeScale = 1.0f;
      gameObject.SetActive(false);
    }
  }
}