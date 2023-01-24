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
    
    private float _horizontalMove;
    private bool _canDoubleJump;
    private bool _isDie;

    private Hero _hero;
    private HeroMove _heroMove;
    private HeroAttack _heroAttack;
    private ColliderCheck _collider;
    public AnimatorController _heroAnimator;
    
    private HeroUI _heroUI;
    private IInputService _input;
    private UnitService _unitService;
    private int _jumpCount;
    private Ray _enemyRay;

    [Inject]
    public void Construct(IInputService inputService, UnitService units)
    {
      _input = inputService;
      _unitService = units;
    }

    public void Init(HeroUI heroUI)
    {
      _heroUI = heroUI;
      _hero = GetComponent<Hero>();
      _heroMove = GetComponent<HeroMove>();
      _heroAttack = GetComponent<HeroAttack>();
      _collider = GetComponent<ColliderCheck>();
      _heroAnimator = GetComponent<AnimatorController>();
      
      InitHealth();
    }

    private void InitHealth()
    {
      _hero.ResetHealth();
      _hero.IsHealthChanged += CheckHealth;
    }
    
    void Update()
    {
      if (!_isDie)
      {
        if (_collider.isLava)
        {
          StartCoroutine(HeroDies());
        }

        ReadInput();
        TryAttack();
      }
    }

    private void ReadInput()
    {
      if (IsJumpButtonDown())
      {
        if (_collider.isGrounded)
        {
          _jumpCount = 1;
        }
        else if (_canDoubleJump)
        {
          _jumpCount = 2;
        }
      }

      _horizontalMove = _input.Axis.x;
    }
    

    private void FixedUpdate()
    {
      _heroMove.Move(_horizontalMove);
      switch (_jumpCount)
      {
        case 1:
          _heroMove.Jump();
          _canDoubleJump = true;
          _jumpCount = 0;
          break;
        case 2:
          _heroMove.DoubleJump();
          _canDoubleJump = false;
          _jumpCount = 0;
          break;
      }
    }

    public void TakeDamage(int damage)
    {
      _hero.TakeDamage(damage, _heroAnimator);
    }
    private void TryAttack()
    {
      if (_input.IsAttackButtonUp())
      {
        if (!_heroAnimator.IsAttacking)
        {
          _heroAnimator.PlayAttack();
        }
        if (_collider.GetEnemies() != null)
        {
          _heroAttack.Attack(_collider.GetEnemies(), _unitService.EnemyController, _hero._attackDamage);
        }
      }
    }

    private void CheckHealth()
    {
      _heroUI.UpdateBar(_hero.CurrentPlayerHp, _hero.MaxPlayerHp);
      if (_hero.CurrentPlayerHp <= 0 && !_isDie)
      {
        StartCoroutine(HeroDies());
      }
    }

    private IEnumerator HeroDies()
    {
      _isDie = true;
      _heroAnimator.PlayDeath();
      yield return new WaitForSeconds(1f);
      IsDead?.Invoke();
    }

    private bool IsJumpButtonDown() =>
      _input.Axis.y > 0.5 && _jumpCount == 0;
    
  }
}