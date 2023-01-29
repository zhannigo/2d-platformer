using System.Collections;
using Common.Infrastructure.Data;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure
{
  public class Key:MonoBehaviour
  {
    [SerializeField] private GameObject _keyObject;
    [SerializeField] private AudioSource PickUpSound;
    private IPersistentProgressService _progressService;
    private bool _picked;

    [Inject]
    public void Contruct(IPersistentProgressService progressService) => 
      _progressService = progressService;

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.tag == "Player")
      {
        PickUp();
      }
    }

    private void PickUp()
    {
      if(_picked)
        return;
      _picked = true;
      
      UpdateWorldData();
      PlayFx();
      HideLoot();
      StartCoroutine(StartDestroyTime());
    }

    private void UpdateWorldData() => 
      _progressService.Progress.WorldData.KeyData.Add(1);

    private void HideLoot() => 
      _keyObject.SetActive(false);

    private void PlayFx() => 
      PickUpSound.Play();

    private IEnumerator StartDestroyTime()
    {
      yield return new WaitForSeconds(0.5f);
      Destroy(gameObject);
    }
  }
}