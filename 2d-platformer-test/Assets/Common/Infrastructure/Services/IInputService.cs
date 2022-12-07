using UnityEngine;

namespace Common.Infrastructure.Services
{
  public interface IInputService
  {
    Vector2 Axis { get; }
    bool IsAttackButtonUp();
    bool IsPauseButtonUp();
  }
}