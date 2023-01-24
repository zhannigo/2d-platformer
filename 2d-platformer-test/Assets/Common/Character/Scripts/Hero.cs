using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Character.Scripts
{
  [RequireComponent(typeof(AnimatorController))]
  public class Hero : MonoBehaviour
  {
    public event Action IsHealthChanged;
    public int MaxPlayerHp { get ; set; }
    public int _attackDamage = 1;

    public float _runSpeed = 6;
    public float _jumpForce = 2;

    private int _currentPlayerHp;


    public int CurrentPlayerHp
    {
      get => _currentPlayerHp;
      set
      {
        _currentPlayerHp = _currentPlayerHp >= 0 ? value : 0;
        IsHealthChanged?.Invoke();
      }
    }

    public void ResetHealth() => 
      CurrentPlayerHp = MaxPlayerHp;

    public void TakeDamage(int damage, AnimatorController animator)
    {
      if (CurrentPlayerHp > 0)
      {
        CurrentPlayerHp -= damage;
        animator.PlayHit();
      }
    }

    public void Construct(int heroDataMaxHp) => 
      MaxPlayerHp = heroDataMaxHp;
  }
}