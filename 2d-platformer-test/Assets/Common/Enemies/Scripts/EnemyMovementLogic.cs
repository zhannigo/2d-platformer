using System;
using System.Collections;
using System.Collections.Generic;
using Aoiti.Pathfinding;
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

    Vector2 targetNode; //target in 2D space
    List <Vector2> path;
    public List<Vector2> pathLeftToGo= new List<Vector2>();
    private bool _move;

    void Start() => 
      pathfinder = new Pathfinder<Vector2>(GetDistance,GetNeighbourNodes,1000); 
    //increase patience or gridSize for larger maps

    private void Update()
    {
      if (_move)
      {
        if (pathLeftToGo.Count == 0) 
        {
          Vector2 target = GetNextTarget();
          GetMoveCommand(target);
        }

        if (pathLeftToGo.Count > 0)
        {
          StartCoroutine(MoveUpdate());
        }
      }
    }

    public void MoveOn() => 
      _move = true;

    private IEnumerator MoveUpdate()
    {
      Vector3 dir = (Vector3)pathLeftToGo[0] - transform.position;
        transform.position += dir.normalized * (speed * Time.deltaTime);
        if (((Vector2)transform.position - pathLeftToGo[0]).sqrMagnitude < speed * speed)
        {
          transform.position = pathLeftToGo[0];
          pathLeftToGo.RemoveAt(0);
        }

      yield return null;
    }

    private Vector2 GetNextTarget()
    {
      Vector2 target = new Vector2(transform.position.x + _leftDistance, transform.position.y + _upDistance);
      _leftDistance = -_leftDistance;
      Flip();
      return target;
    }

    private void Flip()
    {
      Vector3 theScale = transform.localScale;
      theScale.x *= -1;
      transform.localScale = theScale;
    }

    private void LookAt(Vector3 target) => 
      transform.right = target - transform.position;


    public void GetMoveCommand(Vector2 target)
    {
      Vector2 closestNode = GetClosestNode(transform.position);
      if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path)) //Generate path between two points on grid that are close to the transform position and the assigned target.
      {
        if (searchShortcut && path.Count>0)
          pathLeftToGo = ShortenPath(path);
        else
        {
          pathLeftToGo = new List<Vector2>(path);
          if (!snapToGrid) pathLeftToGo.Add(target);
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