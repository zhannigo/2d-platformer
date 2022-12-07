using UnityEngine;

namespace Common.Infrastructure
{
  public interface IInputService
  {
    Vector2 Axis { get; }
    bool IsAttackButtonUp();
    bool IsPauseButtonUp();
  }
}