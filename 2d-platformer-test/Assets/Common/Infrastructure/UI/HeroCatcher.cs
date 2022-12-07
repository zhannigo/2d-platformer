using System;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure
{
  [RequireComponent(typeof(CinemachineVirtualCamera))]
  public class HeroCatcher : MonoBehaviour
  {
    private CinemachineVirtualCamera VCamera => 
      GetComponent<CinemachineVirtualCamera>();
    private UnitService _units;

    [Inject]
    public void Construct(UnitService units) => 
      _units = units;


    private void Start()
    {
      VCamera.Follow = _units.HeroController.transform;
    }

  }
}