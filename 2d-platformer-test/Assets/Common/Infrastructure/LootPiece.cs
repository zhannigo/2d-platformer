using System;
using System.Collections;
using Common.Infrastructure.Data;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure
{
  public class LootPiece: MonoBehaviour
  {
    public GameObject _lootObject;
    public GameObject PickUpFx;
    public int _lootValue;

    private bool _picked;
    private IPersistentProgressService _progressService;

    [Inject]
    public void Contruct(IPersistentProgressService progressService) => 
      _progressService = progressService;

    private void OnTriggerEnter2D(Collider2D other) => 
      PickUp();

    private void PickUp()
    {
      if(_picked)
        return;
      _picked = true;
      
      UpdateWorldData();
      HideLoot();
      //PlayFx();
      StartCoroutine(StartDestroyTime());
    }

    private void UpdateWorldData()
    {
      _progressService.Progress.WorldData.LootData.Add(_lootValue);
      Debug.Log(_progressService.Progress.WorldData.LootData.Collected);
    }

    private void HideLoot() => 
      _lootObject.SetActive(false);

    private void PlayFx() => 
      Instantiate(PickUpFx, transform.position, Quaternion.identity, gameObject.transform);

    private IEnumerator StartDestroyTime()
    {
      yield return new WaitForSeconds(0.5f);
      Destroy(gameObject);
    }
  }
}