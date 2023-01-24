using System;
using System.Collections;
using Common.Character.Scripts;
using UnityEngine;

namespace Common.Enemies.Scripts
{
  [RequireComponent(typeof(EnemyMovementLogic))]
  public class Enemy : MonoBehaviour
  {
    [SerializeField] public AudioSource AttackSound;
    [SerializeField] public EnemyMovementLogic MoveController;
    public Animator EnemyAnimator => GetComponent<Animator>();

    public event Action <Enemy, string> IsEnemyDead;

    public string Id { get; set; }

    public float Health { get; set; }
    public Transform HeroTrasform { get; set; }


    public bool IsHeroTriggered
    {
      get => _isHeroTriggered;
      set
      {
        if (value)
        {
          EnemyEventManager.HeroIsFounded(this);
        }
        else
        {
          EnemyEventManager.HeroisLost(this);
        }
        _isHeroTriggered = value;
      }
    }

    public EnemyState State
    {
      get => _state;
      set
      {
        _state = value;
        EnemyEventManager.StateIsChanged(this);
      }
    }

    public int Damage = 1;
    private EnemyState _state = EnemyState.Idle;
    private bool _isHeroTriggered;
    public Hero hero;

    public void TakeDamage(int damage)
    {
      IsEnemyDead?.Invoke(this, Id);
      StartCoroutine(MakeAnimation());
    }
    private IEnumerator MakeAnimation()
    {
      EnemyAnimator.SetTrigger("Dead");
      yield return new WaitForSeconds(0.5f);
      Destroy(gameObject);
    }
  }
}