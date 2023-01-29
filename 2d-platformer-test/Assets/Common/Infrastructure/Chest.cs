using System.Collections;
using System.Collections.Generic;
using Common.Infrastructure.Data;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure
{
  [RequireComponent(typeof(Animator))]
  public class Chest : MonoBehaviour
  {
    [SerializeField] private int _keyValue;
    [SerializeField] private List<GameObject> LootPrefabs;

    private Animator _animator;
    private IPersistentProgressService _progressService;
    private static readonly int Open = Animator.StringToHash("Open");
    private static readonly int Empty = Animator.StringToHash("Empty");

    public void Start() => 
      _animator = GetComponent<Animator>();

    [Inject]
    public void Contruct(IPersistentProgressService progressService) => 
      _progressService = progressService;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.tag == "Player" && isKey())
      {
        OpenChest();
        _progressService.Progress.WorldData.KeyData.Subtract(_keyValue);
      }
    }

    private void OpenChest()
    {
      _animator.SetBool(Open, true);
      GameObject lootPrefab = LootPrefabs[Random.Range(0, LootPrefabs.Count)];
      float posX = Random.Range(-2,2);
      Vector3 treasurePos  = new Vector2(transform.position.x + posX, transform.position.y);
      GameObject dropedTreasure = Instantiate(lootPrefab, treasurePos, Quaternion.identity);
      dropedTreasure.GetComponent<LootPiece>().Contruct(_progressService);
      StartCoroutine(StartDestroyTime());
    }

    private bool isKey()
    {
      Debug.Log(_progressService.Progress.WorldData.KeyData.CollectedKey);
      return _progressService.Progress.WorldData.KeyData.CollectedKey >= _keyValue;
    }

    private IEnumerator StartDestroyTime()
    {
      yield return new WaitForSeconds(0.5f);
      _animator.SetBool(Empty, true);
      Destroy(this);
    }
  }
}