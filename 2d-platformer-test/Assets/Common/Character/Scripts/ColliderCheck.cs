using System;
using Common.Enemies.Scripts;
using UnityEngine;

namespace Common.Character.Scripts
{
  public class ColliderCheck : MonoBehaviour
  {
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsLava;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask enemyLayer;

    public bool isGrounded => 
      Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

    public bool isLava =>
      Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsLava);
    public Collider2D[] GetEnemies()=>
     Physics2D.OverlapCircleAll(attackPoint.position, _attackRange, enemyLayer);
  }
}