using System;
using Common.Infrastructure.Services;
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
    private WindowsService _windowService;

    private void Awake() => 
      _windowService = GetComponentInParent<WindowsService>();

    private void Start() => 
      _button.onClick.AddListener(PauseGame);

    private void PauseGame()
    {
      ButtonClickSound.Play();
      _windowService.CreateWindow(WindowId.Pause);
    }
  }
}