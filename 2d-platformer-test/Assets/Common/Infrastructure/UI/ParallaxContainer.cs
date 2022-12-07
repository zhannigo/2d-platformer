using System.Collections.Generic;
using UnityEngine;

namespace Common.Enviroment
{
  public class ParallaxContainer : MonoBehaviour
  {
    [SerializeField] private Vector3 parallaxAxis;
    [SerializeField] private Transform targetCamera;
    [SerializeField] private List<ParallaxView> parallaxViews;
    private Vector3 _deltaPos;
        
    private void LateUpdate() => 
      MoveParallaxViews();

    private void MoveParallaxViews()
    {
      if (targetCamera == null) return;
      var deltaMove = targetCamera.position - _deltaPos;
      var projectedDelta = Vector3.Project(deltaMove, parallaxAxis);
      foreach (var view in parallaxViews)
      {
        view?.MoveParallax(projectedDelta);
      }
      _deltaPos = targetCamera.position;
    }
  }
}