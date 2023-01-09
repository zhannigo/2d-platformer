using System.Collections;
using UnityEngine;

namespace Common.Character.Scripts
{
    [RequireComponent(typeof(AnimatorController), typeof(Rigidbody2D))]
    public class HeroMove : MonoBehaviour
    {
        public ParticleSystem _jumpFX;
        private Vector2 zeroVelocity = Vector2.zero;
        private bool facingRight = true;
        
        private AnimatorController _heroAnimator;
        private Rigidbody2D _rigidbody;
        private Hero _hero;

        public void Awake()
        {
            _hero = GetComponent<Hero>();
            _heroAnimator = GetComponent<AnimatorController>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(float move)
        {
            Vector2 targetVelocity = new Vector2(move * _hero._runSpeed, _rigidbody.velocity.y);
            _rigidbody.velocity = Vector2.SmoothDamp(_rigidbody.velocity, targetVelocity, ref zeroVelocity, .1f);
            Turn(move);
        }

        public void Jump(float verticalMove)
        {
            _rigidbody.AddForce(Vector2.up * (verticalMove * _hero._jumpForce), ForceMode2D.Impulse);
            _heroAnimator.PlayJump();
        }
        public void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _hero._jumpForce, ForceMode2D.Impulse);
            
            _heroAnimator.PlayJump();
            _jumpFX.Play();
        }

        public void DoubleJump() => 
            StartCoroutine(JumpWithPause());

        private IEnumerator JumpWithPause()
        {
            yield return new WaitForSeconds(0.4f);
            //_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _hero._jumpForce * 5);
            _rigidbody.velocity = Vector2.up * (_hero._jumpForce * 10);
            _jumpFX.Play();
            Debug.Log("DoubleJimp ");
        }

        private void Turn(float move)
        {
            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
