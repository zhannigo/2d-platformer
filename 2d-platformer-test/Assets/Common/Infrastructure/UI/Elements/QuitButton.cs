using UnityEngine;
using UnityEngine.UI;

namespace Common.Infrastructure.UI.Elements
{
  public class QuitButton : MonoBehaviour
  {
    [SerializeField] private Button _button;
    [SerializeField] private AudioSource ButtonClickSound;
    private void Start() => 
      _button.onClick.AddListener(QuitGame);

    private void QuitGame()
    {
      ButtonClickSound.Play();
      Application.Quit();
    }
  }
}