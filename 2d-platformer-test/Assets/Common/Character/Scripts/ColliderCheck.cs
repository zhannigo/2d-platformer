using UnityEngine;

namespace Common.Character.Scripts
{
  public class ColliderCheck : MonoBehaviour

  {
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    public bool isGrounded => 
      Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
  }
}