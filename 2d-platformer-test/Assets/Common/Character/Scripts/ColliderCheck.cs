using UnityEngine;

namespace Common.Character.Scripts
{
  public class ColliderCheck : MonoBehaviour
  {
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsLava;

    public bool isGrounded => 
      Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

    public bool isLava =>
      Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsLava);
  }
}