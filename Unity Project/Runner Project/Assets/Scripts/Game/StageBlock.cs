using UnityEngine;
using System.Collections.Generic;

public class StageBlock : MonoBehaviour
{
  private Vector3 _coords;
  private float _xVal;
  private float _zVal;
  private float _w;
  private float _h;
  private StageBlockManager _manager;
  [SerializeField]
  private Material _invalidMaterial;
  [SerializeField]
  private Material _validMaterial;
  private bool _isValid;
  private List<float> _validPositions;
  private Renderer _renderer;

  public void SnapCoords()
  {
    if (_manager == null)
      _manager = GetManager();

    // Getting renderer bounds' size. Get info once
    if (_w == 0f && _h == 0f)
    {
      _renderer = GetComponent<Renderer>();
      _w = _renderer.bounds.extents.x * 2f;
      _h = _renderer.bounds.extents.z * 2f;
    }

    _coords = transform.position;

    _xVal = _coords.x;
    _xVal = Mathf.Round(_xVal / _w) * _w;

    _zVal = _coords.z;
    _zVal = Mathf.Round(_zVal / _h) * _h;

    _coords.x = _xVal;
    _coords.y = 0f;
    _coords.z = _zVal;

    transform.position = _coords;

    CheckValidPosition();
  }

  public void CheckValidPosition()
  {
    if (_manager == null)
      _manager = GetManager();

    _coords = transform.position;
    _xVal = _coords.x;
    _validPositions = _manager.GetValidPositions;

    bool positionFound = false;
    for (int i = 0; i < _validPositions.Count; i++)
    {
      if (_xVal == _validPositions[i])
      {
        positionFound = true;
        break;
      }
    }

    if (_renderer == null)
      _renderer = GetComponent<Renderer>();

    if (positionFound)
    {
      _renderer.material = _validMaterial;
      _manager.SetZValue(_coords.z);
    }
    else
      _renderer.material = _invalidMaterial;

    _isValid = positionFound;
  }

  public void ForceScale()
  {
    transform.localScale = new Vector3(1f, 1f, 1f);
  }

  public void ForceRotation()
  {
    transform.rotation = Quaternion.identity;
  }

  public void SetManager(StageBlockManager comp)
  {
    _manager = comp;
  }

  public bool IsValid
  {
    get
    {
      return _isValid;
    }
  }

  public float GetZValue
  {
    get
    {
      return _zVal;
    }
  }

  public Vector2 GetSize()
  {
    Vector2 size = new Vector2(_w, _h);
    return size;
  }

  private StageBlockManager GetManager()
  {
    return GameObject.FindGameObjectWithTag(BaseValues.TAG_STAGE_CONTROLLER).GetComponent<StageBlockManager>();
  }
}
