using System;
using UnityEngine;

namespace Common.Infrastructure
{
  public interface IHealth 
  {
    void TakeDamage(int attackDamage);
  }
}