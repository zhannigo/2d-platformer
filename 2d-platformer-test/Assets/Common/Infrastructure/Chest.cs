using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    private static readonly int Destroyed = Animator.StringToHash("Destroyed");

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
    }

    private void GenerateTreasure()
    {
      GameObject lootPrefab = LootPrefabs[Random.Range(0, LootPrefabs.Count)];
      float posX = Random.Range(-2,2);
      Vector3 treasurePos  = new Vector2(transform.position.x + posX, transform.position.y);
      DropLoot(lootPrefab, treasurePos);
      _animator.SetBool(Empty, true);
    }

    private void StartDestroy()
    {
      StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
      _animator.SetBool(Destroyed, true);
      yield return new WaitForSeconds(3);
      Destroy(gameObject);
    }

    private void DropLoot(GameObject lootPrefab, Vector3 treasurePos)
    {
      GameObject dropedTreasure = Instantiate(lootPrefab, treasurePos, Quaternion.identity);
      dropedTreasure.GetComponent<LootPiece>().Contruct(_progressService);
    }

    private bool isKey() => 
      _progressService.Progress.WorldData.KeyData.CollectedKey >= _keyValue;
  }
}