using UnityEngine;

namespace Common.Infrastructure
{
  public abstract class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    private const string AttackButton = "Fire";
    private const string PauseButton = "Pause";

    public abstract Vector2 Axis { get; }

    public bool IsAttackButtonUp() => 
      SimpleInput.GetButtonDown(AttackButton);

    public bool IsPauseButtonUp() => 
      SimpleInput.GetButton(PauseButton);

    protected static Vector2 SimpleInputAxis() => 
      new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
  }
  public class MobileInputService : InputService
  {
    public override Vector2 Axis => SimpleInputAxis();
  }
  public class StandaloneInputService : InputService
  {
    public override Vector2 Axis
    {
      get
      {
        var axis = SimpleInputAxis();
        if (axis == Vector2.zero)
          axis = UnityAxis();
        return axis;
      }
    }

    private static Vector2 UnityAxis() => 
      new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
  }
}