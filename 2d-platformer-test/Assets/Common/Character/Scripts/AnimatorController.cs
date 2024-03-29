using System;
using UnityEngine;

namespace Common.Character.Scripts
{
  [RequireComponent(typeof(Animator))]
  public class AnimatorController : MonoBehaviour
  {
    private static readonly int MoveHash = Animator.StringToHash("Walking");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DieHash = Animator.StringToHash("Die");
    private static readonly int JumpHash = Animator.StringToHash("Jumping");

    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _attackStateHash = Animator.StringToHash("Attack");
    private readonly int _walkingStateHash = Animator.StringToHash("Run");
    private readonly int _deathStateHash = Animator.StringToHash("Die");

    public event Action<AnimatorStates> StateEntered;
    public event Action<AnimatorStates> StateExited;

    public AnimatorStates State { get; private set; }

    private Animator Animator;
    private Rigidbody2D Rigidbody;
    private HeroSoundsManager Sounds;
    private ColliderCheck Collider;


    private void Awake()
    {
      Animator = GetComponent<Animator>();
      Rigidbody = GetComponent<Rigidbody2D>();
      Sounds = GetComponent<HeroSoundsManager>();
      Collider = GetComponent<ColliderCheck>();
    }

    private void Update()
    {
      Animator.SetFloat(MoveHash, Mathf.Abs(Rigidbody.velocity.x), 0.1f, Time.deltaTime);
      Animator.SetFloat(JumpHash, Rigidbody.velocity.y, 0.1f, Time.deltaTime);
      if (Collider.isGrounded)
      {
        Sounds.PlaySteps(Rigidbody.velocity.x);
      }
    }

    public bool IsAttacking => State == AnimatorStates.Attack;

    public void PlayHit() => Animator.SetTrigger(HitHash);

    public void PlayAttack()
    {
      Animator.SetTrigger(AttackHash);
      Sounds.PlayAttack();
    }

    public void PlayDeath() => Animator.SetTrigger(DieHash);

    public void ResetToIdle() => Animator.Play(_idleStateHash, -1);

    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
      StateExited?.Invoke(StateFor(stateHash));

    private AnimatorStates StateFor(int stateHash)
    {
      AnimatorStates state;
      if (stateHash == _idleStateHash)
        state = AnimatorStates.Idle;
      else if (stateHash == _attackStateHash)
        state = AnimatorStates.Attack;
      else if (stateHash == _walkingStateHash)
        state = AnimatorStates.Walking;
      else if (stateHash == _deathStateHash)
        state = AnimatorStates.Died;
      else
        state = AnimatorStates.Unknown;
      return state;
    }
  }
}