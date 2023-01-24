using UnityEngine;

namespace Common.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "HeroStaticData", menuName = "StaticData/HeroData")]
  public class HeroStaticData : ScriptableObject
  {
    public GameObject Prefab;
    public int MaxHp;
  }
}