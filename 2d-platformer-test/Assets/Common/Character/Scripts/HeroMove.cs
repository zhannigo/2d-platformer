using Common.Infrastructure;
using UnityEngine;

namespace Common.Character.Scripts
{
    [RequireComponent(typeof(AnimatorController), typeof(Rigidbody2D))]
    public class HeroMove : MonoBehaviour
    {
        private float movementSmoothing = .1f; 
        private Vector2 zeroVelocity = Vector2.zero;
        private bool facingRight = true;
        
        private AnimatorController _heroAnimator;
        private Rigidbody2D _rigidbody;

        public void Awake()
        {
            _heroAnimator = GetComponent<AnimatorController>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(float move)
        {
            Vector2 targetVelocity = new Vector2(move * 10, _rigidbody.velocity.y);
            _rigidbody.velocity = Vector2.SmoothDamp(_rigidbody.velocity, targetVelocity, 
                ref zeroVelocity, movementSmoothing);

            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
        }

        public bool Jump(float verticalMove)
        {
            _rigidbody.AddForce(Vector3.up * verticalMove, ForceMode2D.Impulse);
            _heroAnimator.PlayJump();
            return false;
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
