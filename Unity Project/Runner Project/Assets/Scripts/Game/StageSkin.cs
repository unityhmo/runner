using UnityEngine;

public class StageSkin : MonoBehaviour
{
  [SerializeField]
  private Material _assetsMaterial;
  [SerializeField]
  private GameObject _middleLaneLines;
  [SerializeField]
  private GameObject _middleLaneNoLines;
  [SerializeField]
  private GameObject _middleFrontLedge;
  [SerializeField]
  private GameObject _middleBackLedge;
  [SerializeField]
  private GameObject _sideLaneLines;
  [SerializeField]
  private GameObject _sideLaneNoLines;
  [SerializeField]
  private GameObject _sideFrontLedge;
  [SerializeField]
  private GameObject _sideBackLedge;
  [SerializeField]
  private GameObject _ledgeLines;
  [SerializeField]
  private GameObject _ledgeNoLines;

  public GameObject GetAsset(string assetName)
  {
    GameObject asset;
    switch (assetName)
    {
      default:
        asset = new GameObject();
        break;
    }

    Debug.Log("Asking for asset");

    return asset;
  }
}
