using System;
using UnityEngine;

namespace Common.Infrastructure.Services
{
  public abstract class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    private const string AttackButton = "Fire";
    private const string PauseButton = "Pause";

    public abstract Vector2 Axis { get; }

    public bool IsAttackButtonUp() => SimpleInput.GetButtonDown(AttackButton);

    public bool IsPauseButtonUp() => SimpleInput.GetButton(PauseButton);

    protected static Vector2 SimpleInputAxis() => 
      new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
  }
}