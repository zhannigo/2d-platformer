using UnityEngine;

namespace Common.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "TreasureStaticData", menuName = "StaticData/TreasureData")]
  public class TreasureStaticData : ScriptableObject
  {
    public TreasureType _treasureType;
    public GameObject Prefab;
  }
}