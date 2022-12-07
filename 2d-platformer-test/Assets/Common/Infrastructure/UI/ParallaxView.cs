using UnityEngine;

namespace Common.Infrastructure.UI
{
  public class ParallaxView : MonoBehaviour
  {
    [SerializeField] private float movementCoefficient;
        
    public void MoveParallax(Vector3 deltaMove)
    {
      transform.position = CalculatePositionParallax(transform.position, deltaMove, movementCoefficient);
      
    }

    public static Vector3 CalculatePositionParallax(Vector3 currentPos, Vector3 deltaMove, float alpha) => 
      currentPos + deltaMove * alpha;
  }
}