using System;
using System.Collections;
using System.Collections.Generic;
using Aoiti.Pathfinding;
using UnityEditor;
using UnityEngine;

namespace Common.Enemies.Scripts
{
  public class EnemyMovementLogic : MonoBehaviour
  {
    [Header("Navigator options")]
    [SerializeField] float gridSize = 0.5f; //increase patience or gridSize for larger maps
    [SerializeField] float speed = 0.05f; //increase for faster movement
    [SerializeField] private float _leftDistance;
    [SerializeField] private float _upDistance;

    Pathfinder<Vector2> pathfinder; //the pathfinder object that stores the methods and patience

    [Tooltip("The layers that the navigator can not pass through.")]
    [SerializeField] LayerMask obstacles;

    [Tooltip("Deactivate to make the navigator move along the grid only, except at the end when it reaches to the target point. This shortens the path but costs extra Physics2D.LineCast")] 
    [SerializeField] bool searchShortcut =false;

    [Tooltip("Deactivate to make the navigator to stop at the nearest point on the grid.")]
    [SerializeField] bool snapToGrid =false;

    [SerializeField] private bool _right = false;

    Vector2 targetNode; //target in 2D space
    List <Vector2> _path;
    public List<Vector2> pathLeftToGo= new List<Vector2>();
    public List<Vector2> pathToHeroGo= new List<Vector2>();

    private bool _move;
    private bool _moveToHero;
    
    private string MoveHash = "Move";
    private Animator _animator;

    void Start() => 
      pathfinder = new Pathfinder<Vector2>(GetDistance,GetNeighbourNodes,1000); 
    //increase patience or gridSize for larger maps

    private void Update()
    {
      if (_move)
      {
        if (pathLeftToGo.Count == 0)
        {
          Vector2 target = GetNextTarget(transform.position.x + _leftDistance, transform.position.y + _upDistance);
          GetMoveCommand(target);
        }

        if (pathLeftToGo.Count > 0)
        {
          TurnToTarget(pathLeftToGo[0]);
          MoveOnPath(pathLeftToGo);
        }
      }
      else if(_moveToHero)
      {
        MoveOnPath(pathToHeroGo);
      }
    }

    public void MoveOn(Animator enemyEnemyAnimator)
    {
      StartCoroutine(StartMove());
      _animator = enemyEnemyAnimator;
    }

    public void ReturnToTheWay()
    {
      _moveToHero = false;
      _move = true;
    }

    private IEnumerator StartMove()
    {
      yield return new WaitForSeconds(1.5f);
      _move = true;
    }

    private void MoveOnPath(List<Vector2> path)
    {
      Vector3 dir = (Vector3)path[0] - transform.position;
      transform.position += dir.normalized * (speed * Time.deltaTime);
      //transform.Translate(dir.normalized * (speed * Time.fixedDeltaTime));
      //var enemyCurrentSpeed = ((Vector2)transform.position - path[0]).sqrMagnitude;
      float distance = Vector2.Distance(transform.position, path[0]);
      _animator.SetFloat(MoveHash, Mathf.Abs(distance), 0.1f, Time.deltaTime);
      if (distance < speed)
      {
        transform.position = path[0];
        path.RemoveAt(0);
      }
    }

    public void MoveOff() => 
      _move = false;

    public void MoveTo(Transform hero)
    {
      MoveOff();
      Vector3 heroPosition = hero.position;
      TurnToTarget(heroPosition);
      Vector2 target = GetNextTarget(heroPosition.x, heroPosition.y);
      GetMoveToHeroCommand(target);
      _moveToHero = true;
    }

    private Vector2 GetNextTarget(float positionX, float positionY)
    {
      Vector2 target = new Vector2(positionX, positionY);
      _leftDistance = -_leftDistance;
      TurnToTarget(target);
      return target;
    }

    private void TurnToTarget(Vector2 target)
    {
      if (transform.position.x > target.x)
      {
        if (!_right)
        {
          transform.rotation = Quaternion.AngleAxis(0, Vector2.up);
          _right = true;
        }
      }
      else if(_right)
      {
        transform.rotation = Quaternion.AngleAxis(180, Vector2.up);
        _right = false;
      }
    }

    private void GetMoveCommand(Vector2 target)
    {
      Vector2 closestNode = GetClosestNode(transform.position);
      if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out _path)) //Generate path between two points on grid that are close to the transform position and the assigned target.
      {
        if (searchShortcut && _path.Count>0)
          pathLeftToGo = ShortenPath(_path);
        else
        {
          pathLeftToGo = new List<Vector2>(_path);
          if (!snapToGrid) pathLeftToGo.Add(target);
        }
      }
    }
    private void GetMoveToHeroCommand(Vector2 hero)
    {
      Vector2 closestNode = GetClosestNode(transform.position);
      if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(hero), out _path)) //Generate path between two points on grid that are close to the transform position and the assigned target.
      {
        if (searchShortcut && _path.Count>0)
          pathToHeroGo = ShortenPath(_path);
        else
        {
          pathToHeroGo = new List<Vector2>(_path);
          if (!snapToGrid) pathToHeroGo.Add(hero);
        }
      }
    }

    Vector2 GetClosestNode(Vector2 target) 
    {
      return new Vector2(Mathf.Round(target.x/gridSize)*gridSize, Mathf.Round(target.y / gridSize) * gridSize);
    }

    float GetDistance(Vector2 A, Vector2 B) 
    {
      return (A - B).sqrMagnitude; //Uses square magnitude to lessen the CPU time.
    }

    Dictionary<Vector2,float> GetNeighbourNodes(Vector2 pos) 
    {
      Dictionary<Vector2, float> neighbours = new Dictionary<Vector2, float>();
      for (int i=-1;i<2;i++)
      {
        for (int j=-1;j<2;j++)
        {
          if (i == 0 && j == 0) continue;

          Vector2 dir = new Vector2(i, j)*gridSize;
          if (!Physics2D.Linecast(pos,pos+dir, obstacles))
          {
            neighbours.Add(GetClosestNode( pos + dir), dir.magnitude);
          }
        }

      }
      return neighbours;
    }


    List<Vector2> ShortenPath(List<Vector2> path)
    {
      List<Vector2> newPath = new List<Vector2>();
        
      for (int i=0;i<path.Count;i++)
      {
        newPath.Add(path[i]);
        for (int j=path.Count-1;j>i;j-- )
        {
          if (!Physics2D.Linecast(path[i],path[j], obstacles))
          {
            i = j;
            break;
          }
        }
        newPath.Add(path[i]);
      }
      newPath.Add(path[path.Count - 1]);
      return newPath;
    }
  }
}