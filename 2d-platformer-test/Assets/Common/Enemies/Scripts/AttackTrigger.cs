using System;
using System.Collections;
using Common.Character.Scripts;
using UnityEngine;

namespace Common.Enemies.Scripts
{
  public class AttackTrigger : MonoBehaviour
  {
    private Enemy Enemy => GetComponentInParent<Enemy>();
    private void OnTriggerEnter2D(Collider2D col)
    {
      if (col.tag == "Player")
      {
        Enemy.State = EnemyState.Attack;
        HeroController hero = col.gameObject.GetComponent<HeroController>();
        hero.TakeDamage(Enemy.Damage);
      }
    }

    private IEnumerator Attack(Collider2D col)
    {
      yield return new WaitForSeconds(1f);
      
    }
  }
}