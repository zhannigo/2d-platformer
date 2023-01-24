using UnityEngine;

namespace Common.Enemies.Scripts
{
  public class AggroTrigger : MonoBehaviour
  {
    private Enemy Enemy => GetComponentInParent<Enemy>();

    private void OnTriggerEnter2D(Collider2D col)
    {
      if (Enemy != null && col.tag == "Player")
      {
        Enemy.HeroTrasform = col.transform;
        Enemy.IsHeroTriggered = true;
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (Enemy!= null && other.tag == "Player")
      {
        Enemy.IsHeroTriggered = false;
      }
    }
  }
}