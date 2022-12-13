using System;
using System.Collections;
using UnityEngine;

namespace Common.Enemies.Scripts
{
  public class Enemy : MonoBehaviour
  {
    public event Action isEnemyDead;
    public string Id { get; set; }
    private bool Health { get; set; }

    public void TakeDamage(int damage)
    {
      isEnemyDead?.Invoke();
      StartCoroutine(MakeAnimation());
    }
    
    private IEnumerator MakeAnimation()
    {
      yield return new WaitForSeconds(1.5f);
      Destroy(gameObject);
    }
  }
}