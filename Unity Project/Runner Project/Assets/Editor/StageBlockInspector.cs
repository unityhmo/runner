using UnityEditor;

[CustomEditor(typeof(StageBlock))]
[CanEditMultipleObjects]
public class StageBlockInspector : Editor
{
  private StageBlock block;

  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();

    for (var i = 0; i < targets.Length; i++)
    {
      block = targets[i] as StageBlock;
      block.SnapCoords();
      block.ForceScale();
      block.ForceRotation();
    }
  }
}