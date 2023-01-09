using UnityEngine;

namespace Common.Infrastructure.Data
{
  public static class DataExtension
  {
    public static T ToDeserialized<T>(this string json) => 
      JsonUtility.FromJson<T>(json);
    
    public static string ToJson(this object obj) => 
      JsonUtility.ToJson(obj);
  }
}