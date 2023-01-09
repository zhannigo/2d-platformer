using UnityEditor;
using UnityEngine;

namespace Common.Editor
{
  public class Tools
  {
    [MenuItem("Tools/ClearPrefs")]
    public static void ClearPrefs()
    {
      PlayerPrefs.DeleteAll();
      PlayerPrefs.Save();
    }
  }
}