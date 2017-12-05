using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StageBlockManager))]
public class StageBlockManagerInspector : Editor
{
  private StageBlockManager _manager;
  private int _supportedLanes;

  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();

    _manager = target as StageBlockManager;

    if (_manager.GetMaxLanes < _manager.GetMinLanes || _manager.GetMaxLanes % 2 == 0)
    {
      if (_supportedLanes == 0)
        _supportedLanes = _manager.GetMinLanes;

      _manager.SetMaxLanes(_supportedLanes);
    }

    if (_supportedLanes != _manager.GetMaxLanes)
    {
      _manager.GenerateValidPositions();
      _manager.CheckValidPositions();
      _supportedLanes = _manager.GetMaxLanes;
    }

    GUILayout.Label("Create Blocks will result in: " + _manager.GetCreateRows * _manager.GetMaxLanes + " blocks.");
    if (GUILayout.Button("Create Blocks"))
      _manager.CreateBlocks();

    GUILayout.Label("\nRegister blocks layout.\nRemove overlaped and invalid position blocks.\nRecalculate stage length (in blocks).");
    if (GUILayout.Button("Update Layout"))
      _manager.StageCleanup();

    GUILayout.Label("Length (in blocks): " + _manager.SetStageLength());
    GUILayout.Label("Total blocks: " + _manager.GetTotalBlocks());

    GUILayout.Label("\nFinal step. Creates a gameobject with the original\nlayout, but now with skin assets applied.");
    if (GUILayout.Button("Build Stage"))
      _manager.ConstructFinalStage();

    GUILayout.Label("\nDon't forget to save your built stage inside your level Resource.prefab gameObject.");
  }
}
