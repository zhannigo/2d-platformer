using System.Linq;
using Common.Infrastructure;
using Common.Infrastructure.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Editor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelDataEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();
      LevelStaticData spawnData = (LevelStaticData)target;
      if (GUILayout.Button("Collect"))
      {
        spawnData.Spawners = FindObjectsOfType<SpawnMarker>()
          .Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().id, x.monsterType, x.transform.position))
          .ToList();
        spawnData.LevelKey = SceneManager.GetActiveScene().name;
        spawnData.StartPoint = FindObjectOfType<StartPoint>().transform.position;
        spawnData.Treasure = FindObjectsOfType<SpawnTreasureMarker>()
          .Select(x => new TreasureSpawnerData(x.TreasureType, x.transform.position))
          .ToList();
      }
      EditorUtility.SetDirty(target);
    }
  }
}