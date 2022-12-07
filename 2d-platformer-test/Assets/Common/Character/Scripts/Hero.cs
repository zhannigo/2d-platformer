using System;
using UnityEngine;

namespace Common.Character.Scripts
{
  public class Hero : MonoBehaviour
  {
    public event Action isHealthChanged;
    
    private HealthBar healthBar;

    private int _currentPlayerHP;
    private int _maxPlayerHP;

    public int CurrentPlayerHP
    {
      get => _currentPlayerHP;

      set
      {
        Debug.Log($"{value}, {_currentPlayerHP}");
        _currentPlayerHP = _currentPlayerHP >= 0 ? value : 0;
      }
    }

    public void ResetHealth()
    {
      //healthBar.SetMaxHealth(_maxPlayerHP);
      CurrentPlayerHP = _maxPlayerHP;
    }

    public void TakeDamage(int damage)
    {
      if (CurrentPlayerHP > 0)
      {
        CurrentPlayerHP -= damage;
        isHealthChanged?.Invoke();
      }
    }
  }
}