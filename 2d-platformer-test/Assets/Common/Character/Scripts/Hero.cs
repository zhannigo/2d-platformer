using System;
using UnityEngine;

namespace Common.Character.Scripts
{
  public class Hero : MonoBehaviour
  {
    public event Action IsHealthChanged;

    private int _currentPlayerHp;
    public int _maxPlayerHP;

    public int _attackDamage;

    public float _runSpeed = 6;
    public float _jumpForce = 2;

    public int CurrentPlayerHp
    {
      get => _currentPlayerHp;
      set => _currentPlayerHp = _currentPlayerHp >= 0 ? value : 0;
    }

    public void ResetHealth() => 
      CurrentPlayerHp = _maxPlayerHP;

    public void TakeDamage(int damage)
    {
      if (CurrentPlayerHp > 0)
      {
        CurrentPlayerHp -= damage;
        IsHealthChanged?.Invoke();
      }
    }
  }
}