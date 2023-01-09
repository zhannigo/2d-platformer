using System;
using System.Collections;
using UnityEngine;

namespace Common.Enemies.Scripts
{
  [RequireComponent(typeof(EnemyMovementLogic))]
  public class Enemy : MonoBehaviour
  {
    [SerializeField] public EnemyMovementLogic MoveController;
    private static float _radius;
    private static Vector2 _direction;
    private LayerMask _playerLayer;
    private float _distance;
    private EnemyState _state = EnemyState.Idle;
    public event Action <string> isEnemyDead;
    public string Id { get; set; }

    public EnemyState State
    {
      get => _state;
      set
      {
        _state = value;
        //isChangedState?.Invoke(this);
        EventManager.StateIsChanged(this);
      }
    }

    public float Health { get; set; }
    public void HeroCheck()
    {
      if (Physics2D.CircleCast(transform.position, _radius, _direction, _distance, _playerLayer))
      {
        State = EnemyState.Attack; 
      }
    }

    public void TakeDamage(int damage)
    {
      isEnemyDead?.Invoke(Id);
      StartCoroutine(MakeAnimation());
    }

    private IEnumerator MakeAnimation()
    {
      yield return new WaitForSeconds(1.5f);
      Destroy(gameObject);
    }
  }
}