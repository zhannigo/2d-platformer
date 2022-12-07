using UnityEngine;
using UnityEngine.UI;

namespace Common.Infrastructure.Elements
{
  public class QuitButton : MonoBehaviour
  {
    public Button _button;
    private void Start() => 
      _button.onClick.AddListener(QuitGame);

    private void QuitGame() =>
      Application.Quit();
  }
}