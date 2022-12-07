using UnityEngine;

namespace Common.Infrastructure.Services
{
  public class MobileInputService : InputService
  {
    public override Vector2 Axis => SimpleInputAxis();
  }
}