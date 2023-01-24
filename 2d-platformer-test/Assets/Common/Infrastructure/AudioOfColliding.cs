using UnityEngine;

namespace Common.Infrastructure
{
    public class AudioOfColliding : MonoBehaviour
    {
        [SerializeField] private AudioSource audioOfColliding;
        [SerializeField] private LayerMask enemyLayer;
        private string _playerTag = "Player";
    
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == _playerTag || col.gameObject.layer == enemyLayer)
            {
                audioOfColliding.Play();
            }
        }
    }
}
