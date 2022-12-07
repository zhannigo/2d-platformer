using Common.Infrastructure;
using UnityEngine;
using Zenject;

namespace Common.Character.Scripts
{
  public class HeroController : MonoBehaviour
  {
    public Transform groundCheck;
    public float groundRadius; // static data
    public LayerMask whatIsGround; // static data
    public float runSpeed = 60f; // static data
    public float jumpForce = 30f; //static data

    private float verticalMove;
    private bool isGrounded;
    private float horizontalMove;
    private bool _jump;

    private Rigidbody2D heroRigidbody;
    private HeroMove _heroMove;
    private HeroAttack _heroAttack;
    private IInputService _input;

    [Inject]
    void Construct(IInputService input) => _input = input;

    private void Awake() => heroRigidbody = GetComponent<Rigidbody2D>();
        
    void Update()
    {
      horizontalMove = _input.Axis.x * runSpeed;
      verticalMove = _input.Axis.y * jumpForce;
            
      if (IsJumpButtonUp() && isGrounded)
      {
        _jump = true;
      }

      if (_input.IsAttackButtonUp())
      {
        _heroAttack.Attack();
      }
    }
    private bool IsJumpButtonUp() => _input.Axis.y > 0.1;

    private void FixedUpdate()
    {
      _heroMove.Move(heroRigidbody, horizontalMove);
      isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
      if (_jump)
      {
        _jump = _heroMove.Jump(heroRigidbody, verticalMove);
      }
    }
  }
}