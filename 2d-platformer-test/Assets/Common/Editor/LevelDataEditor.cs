using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Common.Editor
{
  [CustomEditor(typeof(LevelData))]
  public class LevelDataEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();
      LevelData spawnData = (LevelData)target;
      if (GUILayout.Button("Collect"))
      {
        spawnData.Spawners = FindObjectsOfType<SpawnMarker>()
          .Select(x => new EnemyData(x.monsterType, x.transform.position))
          .ToList();
      }
      EditorUtility.SetDirty(target);
    }
  }
}