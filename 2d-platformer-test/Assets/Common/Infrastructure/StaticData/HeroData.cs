using UnityEngine;

namespace Common.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/HeroData")]
  public class HeroData : ScriptableObject
  {
    public GameObject Prefab;
  }
}