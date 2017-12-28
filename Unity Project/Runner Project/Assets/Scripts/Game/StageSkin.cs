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
  [SerializeField]
  private GameObject _streetLight;
  [SerializeField]
  private GameObject _goalMid;
  [SerializeField]
  private GameObject _goalSide;

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
      case "mid_front":
        asset = _middleLedgeFront;
        break;
      case "mid_back":
        asset = _middleLedgeBack;
        break;
      case "side":
        asset = _sideLane;
        break;
      case "side_alt":
        asset = _sideLaneAlt;
        break;
      case "side_front":
        asset = _sideLedgeFront;
        break;
      case "side_back":
        asset = _sideLedgeBack;
        break;
      case "ledge":
        asset = _ledge;
        break;
      case "ledge_alt":
        asset = _ledgeAlt;
        break;
      case "street_light":
        asset = _streetLight;
        break;
      case "goal_mid":
        asset = _goalMid;
        break;
      case "goal_side":
        asset = _goalSide;
        break;
      default:
        asset = _middleLedgeFront;
        break;
    }

    return asset;
  }

  public GameObject StreetLightAsset
  {
    get
    {
      return _streetLight;
    }
  }

  public Material SkinMaterial
  {
    get
    {
      return _assetsMaterial;
    }
  }
}
