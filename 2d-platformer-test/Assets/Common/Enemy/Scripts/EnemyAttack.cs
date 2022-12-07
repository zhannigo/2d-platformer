using Common.Character.Scripts;
using Common.Infrastructure;
using UnityEngine;

namespace Common.Enemy.Scripts
{
  public class EnemyAttack : MonoBehaviour
  {
    private int _damage;

    public void Attack()
    {
      
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      if (other.gameObject.tag == "Player")
      {
        other.gameObject.GetComponent<IHealth>().TakeDamage(_damage);
      }
    }
  }
}