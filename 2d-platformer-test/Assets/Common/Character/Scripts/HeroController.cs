using System;
using System.Collections;
using Common.Infrastructure;
using Common.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Common.Character.Scripts
{
  public class HeroController : MonoBehaviour
  {
    public float runSpeed = 1f;
    public float jumpForce = 1f;

    public event Action IsDead;

    private bool isGrounded;
    private float verticalMove;
    private float horizontalMove;
    private bool _jump;
    private bool _isDie;

    private Hero _hero;
    private HeroMove _heroMove;
    private HeroAttack _heroAttack;
    private ColliderCheck _collider;

    private IInputService _input;
    private UnitService _unitService;
    
    [Inject]
    public void Construct(IInputService inputService, UnitService units)
    {
      _input = inputService;
      _unitService = units;
      Init();
    }

    private void Init()
    {
      _hero = GetComponent<Hero>();
      _heroMove = GetComponent<HeroMove>();
      _heroAttack = GetComponent<HeroAttack>();
      _collider = GetComponent<ColliderCheck>();
      InitHealth();
    }

    private void InitHealth()
    {
      _hero.ResetHealth();
      _hero.isHealthChanged += CheckHealth;
    }

    void Update()
    {
      if (_input == null)
      {
        return;
      }
      horizontalMove = _input.Axis.x * runSpeed;
      verticalMove = _input.Axis.y * jumpForce;
            
      if (IsJumpButtonUp() && isGrounded)
      {
        _jump = true;
      }

      if (_input.IsAttackButtonUp())
      {
        string id = _heroAttack.PlayAttack();
        _unitService.EnemyController?.TakeDamage(id, _heroAttack._attackDamage);
      }
    }

    private bool IsJumpButtonUp() => 
      _input.Axis.y > 0.1 && !_jump;

    private void FixedUpdate()
    {
      _heroMove?.Move(horizontalMove);
      isGrounded = _collider.isGrounded;
      if (_jump)
      {
        _jump = _heroMove.Jump(verticalMove);
      }
    }

    private void CheckHealth()
    {
      if (_hero.CurrentPlayerHP <= 0 && !_isDie)
      {
        _isDie = true;
        StartCoroutine(MakeAnimation());
      }
    }

    private IEnumerator MakeAnimation()
    {
      yield return new WaitForSeconds(1.5f);
      //animator.PlayDeath();
      IsDead?.Invoke();
    }
  }
}