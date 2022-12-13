using System;
using System.Collections;
using Common.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Common.Character.Scripts
{
  public class HeroController : MonoBehaviour
  {
    public event Action IsDead;

    private float _verticalMove;
    private float _horizontalMove;
    private bool _isGrounded;
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
      _horizontalMove = _input.Axis.x;
      _verticalMove = _input.Axis.y;
            
      if (IsJumpButtonUp() && _isGrounded)
      {
        _jump = true;
      }

      if (_input.IsAttackButtonUp())
      {
        string id = _heroAttack.PlayAttack();
        if ( id != null)
        {
         _unitService.EnemyController?.TakeDamage(id, _heroAttack._attackDamage);
        }
      }
    }

    private bool IsJumpButtonUp() => 
      _input.Axis.y > 0.1 && !_jump;

    private void FixedUpdate()
    {
      _heroMove?.Move(_horizontalMove);
      _isGrounded = _collider.isGrounded;
      if (_collider.isLava)
      {
        StartCoroutine(MakeAnimation());
      }
      if (_jump)
      {
        _jump = _heroMove.Jump(_verticalMove);
      }
    }

    private void CheckHealth()
    {
      if (_hero.CurrentPlayerHP <= 0 && !_isDie)
      {
        StartCoroutine(MakeAnimation());
      }
    }

    private IEnumerator MakeAnimation()
    {
      _isDie = true;
      yield return new WaitForSeconds(1.5f);
      //animator.PlayDeath();
      IsDead?.Invoke();
    }
  }
}