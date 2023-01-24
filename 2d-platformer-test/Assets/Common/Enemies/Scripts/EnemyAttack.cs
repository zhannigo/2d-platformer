using Common.Character.Scripts;
using UnityEngine;

namespace Common.Enemies.Scripts
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
        other.gameObject.GetComponent<HeroController>().TakeDamage(_damage);
      }
    }
  }
}