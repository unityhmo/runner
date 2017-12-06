using UnityEngine;

public class StageSkin : MonoBehaviour
{
  [SerializeField]
  private Material _assetsMaterial;
  [SerializeField]
  private GameObject _middleLane;
  [SerializeField]
  private GameObject _middleLaneAlt;
  [SerializeField]
  private GameObject _middleLedgeFront;
  [SerializeField]
  private GameObject _middleLedgeBack;
  [SerializeField]
  private GameObject _sideLane;
  [SerializeField]
  private GameObject _sideLaneAlt;
  [SerializeField]
  private GameObject _sideLedgeFront;
  [SerializeField]
  private GameObject _sideLedgeBack;
  [SerializeField]
  private GameObject _ledge;
  [SerializeField]
  private GameObject _ledgeAlt;

  public GameObject GetAsset(string assetName)
  {
    GameObject asset;
    switch (assetName)
    {
      case "mid":
        asset = _middleLane;
        break;
      case "mid_alt":
        asset = _middleLaneAlt;
        break;
      case "side":
        asset = _sideLane;
        break;
      case "side_alt":
        asset = _sideLaneAlt;
        break;
      default:
        asset = _middleLedgeFront;
        break;
    }

    return asset;
  }

  public Material SkinMaterial
  {
    get
    {
      return _assetsMaterial;
    }
  }
}
