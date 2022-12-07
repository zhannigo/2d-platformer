using Common.Infrastructure;
using UnityEngine;
using Zenject;

namespace Common.Character.Scripts
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _heroAnimator;

        public Transform groundCheck;
        public float groundRadius; // static data
        public LayerMask whatIsGround; // static data
        public float runSpeed = 60f; // static data
        public float jumpForce = 30f; //static data

        private readonly float movementSmoothing = .1f; // static data
        
        private float horizontalMove;
        private float verticalMove;
        private Vector2 zeroVelocity = Vector2.zero;

        private bool _jump;
        private bool isGrounded;
        private bool facingRight = true;

        private Rigidbody2D heroRigidbody;
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
        }

        private bool IsJumpButtonUp() => _input.Axis.y > 0.1;

        private void FixedUpdate()
        {
            Move(horizontalMove);
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

            if (_jump)
            {
                Jump();
            }
        }
        public void Move(float move)
        {
            Vector2 targetVelocity = new Vector2(move * 10, heroRigidbody.velocity.y);
            heroRigidbody.velocity = Vector2.SmoothDamp(heroRigidbody.velocity, targetVelocity, ref zeroVelocity, movementSmoothing);

            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
        }
        private void Jump()
        {
            heroRigidbody.AddForce(Vector3.up * verticalMove, ForceMode2D.Impulse);
            _jump = false;
        }
        public void Flip()
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
