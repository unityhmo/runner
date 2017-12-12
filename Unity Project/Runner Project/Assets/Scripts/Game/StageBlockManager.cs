using System;
using System.Collections.Generic;
using UnityEngine;

public class StageBlockManager : MonoBehaviour
{
  [SerializeField]
  private GameObject _blockPrefab;
  [SerializeField]
  private int _streetLightFrequency = 0;
  [SerializeField]
  private GameObject _stageSkin;
  private int _minLanes = 3;
  [SerializeField]
  private int _maxLanes; // Only odd numbers please
  [SerializeField]
  private int _createRows;
  private float[] _validPositions;
  private GameObject[] _blocks;
  private float _blockWidth;
  private float _blockHeight;
  private bool _emptyZ = true;
  private float _lowestZ;
  private float _highestZ;
  private int _stageLength;
  private float _blockColliderHeight = 3f;
  private int _nextStreetLight;

  // Visual assets variables
  private GameObject _bgo;
  private BoxCollider _bcollider;
  private Vector3 _bsize;
  private Vector3 _bcenter;
  private GameObject _basset;
  private Renderer _brenderer;

  public void CreateBlocks()
  {
    GameObject newBlock;
    Vector3 blockPosition;
    int currentLane = 0;
    int currentRow = 0;
    int laneJump = 0;

    for (int i = 0; i < _maxLanes * _createRows; i++)
    {
      newBlock = Instantiate(_blockPrefab) as GameObject;

      GetBlockSizes();

      blockPosition = Vector3.zero;
      blockPosition.x = (float)Math.Round(laneJump * _blockWidth, 3);
      blockPosition.z = (float)Math.Round(currentRow * _blockHeight, 3);

      SetZValue(blockPosition.z);

      newBlock.transform.position = blockPosition;
      newBlock.transform.parent = transform;
      newBlock.GetComponent<StageBlock>().SetManager(this);

      if (laneJump != 0)
      {
        if (laneJump > 0)
          laneJump = -laneJump;
        else if (laneJump < 0)
          laneJump = Mathf.Abs(laneJump) + 1;
      }
      else
        laneJump++;

      currentLane++;
      if (currentLane == _maxLanes)
      {
        currentLane = 0;
        laneJump = 0;
        currentRow++;
      }
    }
    _createRows = 0;
  }

  public void GenerateValidPositions()
  {
    float xPosition = 0f;
    int laneJump = 0;

    GetBlockSizes();

    _validPositions = new float[GetMaxLanes];
    for (int i = 0; i < _maxLanes; i++)
    {
      xPosition = (float)Math.Round(laneJump * _blockWidth, 3);
      _validPositions[i] = xPosition;

      if (laneJump != 0)
      {
        if (laneJump > 0)
          laneJump = -laneJump;
        else if (laneJump < 0)
          laneJump = Mathf.Abs(laneJump) + 1;
      }
      else
        laneJump++;
    }

    Array.Sort(_validPositions);
  }

  public void CheckValidPositions(bool andRemove = false)
  {
    StageBlock blockComponent;
    _blocks = GetTotalBlocks();

    GameObject block;
    for (int i = 0; i < _blocks.Length; i++)
    {
      block = _blocks[i];
      blockComponent = block.GetComponent<StageBlock>();
      blockComponent.CheckValidPosition();

      if (andRemove && !blockComponent.IsValid)
        DestroyImmediate(block);
    }
  }

  public float[] GetValidPositions
  {
    get
    {
      return _validPositions;
    }
  }

  public Dictionary<string, GameObject> StageCleanup()
  {
    _emptyZ = true;

    // First we remove all invalid blocks
    CheckValidPositions(true);

    // Now we removed overlapped blocks and return as result
    return RemoveOverlapped();
  }

  public void SetMaxLanes(int value)
  {
    _maxLanes = value;
  }

  public void SetZValue(float value)
  {
    if (_emptyZ)
    {
      _highestZ = value;
      _lowestZ = value;
    }
    else
    {
      if (value > _highestZ)
        _highestZ = value;
      else if (value < _lowestZ)
        _lowestZ = value;
    }

    _emptyZ = false;
  }

  private void GetBlockSizes()
  {
    if (_blockWidth == 0f && _blockHeight == 0f)
    {
      Renderer renderer = _blockPrefab.GetComponent<Renderer>();
      _blockWidth = (float)Math.Round(renderer.bounds.extents.x * 2f, 3);
      _blockHeight = (float)Math.Round(renderer.bounds.extents.z * 2f, 3);
    }
  }

  public int GetMaxLanes
  {
    get
    {
      return _maxLanes;
    }
  }

  public int GetMinLanes
  {
    get
    {
      return _minLanes;
    }
  }

  public int GetCreateRows
  {
    get
    {
      return _createRows;
    }
  }

  public float GetLowestZ
  {
    get
    {
      return _lowestZ;
    }
  }

  public float GetHighestZ
  {
    get
    {
      return _highestZ;
    }
  }

  public float GetBlockHeight
  {
    get
    {
      return _blockHeight;
    }
  }

  public GameObject[] GetTotalBlocks()
  {
    _blocks = GameObject.FindGameObjectsWithTag(BaseValues.TAG_STAGE_BLOCK);

    return _blocks;
  }

  public int SetStageLength()
  {
    _stageLength = 0;

    if (GetTotalBlocks().Length > 0)
    {
      _stageLength = (int)Mathf.Round((GetHighestZ - GetLowestZ) / GetBlockHeight) + 1;
    }

    return _stageLength;
  }

  public int StageLength
  {
    get
    {
      return _stageLength;
    }
  }

  public void ForcePosition()
  {
    transform.position = Vector3.zero;
  }

  private Dictionary<string, GameObject> RemoveOverlapped()
  {
    Dictionary<string, GameObject> blocksCoords = new Dictionary<string, GameObject>();

    GameObject temp;
    Vector3 pos;
    string label;

    _blocks = GameObject.FindGameObjectsWithTag(BaseValues.TAG_STAGE_BLOCK);

    GameObject block;
    for (int i = 0; i < _blocks.Length; i++)
    {
      block = _blocks[i];
      pos = block.transform.position;
      label = pos.x + "_" + pos.z;
      temp = null;

      if (blocksCoords.TryGetValue(label, out temp))
        DestroyImmediate(block);
      else
        blocksCoords.Add(label, block);
    }

    return blocksCoords;
  }

  public GameObject ConstructFinalStage()
  {
    Vector2 coords;
    Vector2 indexes;
    int iRows;
    int iLane;
    string label;
    float z = GetLowestZ;

    GameObject result = new GameObject();
    result.name = BaseValues.LABEL_STAGE_BLOCKS_RESULT; ;
    StageSkin skin = _stageSkin.GetComponent<StageSkin>();

    Dictionary<string, GameObject> blocksCoords = StageCleanup();

    _bsize = new Vector3(_blockWidth, _blockColliderHeight, _blockHeight);
    _bcenter = new Vector3(0f, -_blockColliderHeight / 2, 0f);
    _nextStreetLight = _streetLightFrequency;

    for (iRows = 0; iRows < StageLength; iRows++)
    {
      for (iLane = 0; iLane < GetValidPositions.Length; iLane++)
      {
        coords.x = GetValidPositions[iLane];
        coords.y = z;
        indexes.x = iLane;
        indexes.y = iRows;
        label = coords.x + "_" + coords.y;

        if (blocksCoords.ContainsKey(label))
        {
          ConstructBlockAssets(blocksCoords, coords, indexes, skin, result.transform);
        }
      }
      z += GetBlockHeight;

      if (_streetLightFrequency > 0 && iRows == _nextStreetLight - 1)
        _nextStreetLight += _streetLightFrequency;
    }

    gameObject.SetActive(false);

    return result;
  }

  private void ConstructBlockAssets(Dictionary<string, GameObject> blocksCoords, Vector2 coords, Vector2 indexes, StageSkin skin, Transform parent)
  {
    Vector2 adjacentCoords;

    _bgo = new GameObject();
    _bgo.transform.position = new Vector3(coords.x, 0f, coords.y);
    _bgo.name = "block: " + indexes.x + "_" + indexes.y;
    _bcollider = _bgo.AddComponent<BoxCollider>();
    _bcollider.size = _bsize;
    _bcollider.center = _bcenter;
    _bgo.transform.parent = parent;

    // Ground
    CreateLayoutBlock(indexes, coords, _bgo.transform, skin);

    // Back
    adjacentCoords = new Vector2(coords.x, coords.y + _blockHeight);
    if (indexes.y < StageLength - 1 && !blocksCoords.ContainsKey(adjacentCoords.x + "_" + adjacentCoords.y))
    {
      CreateLayoutBlock(indexes, adjacentCoords, _bgo.transform, skin, "back");
    }

    // Front
    adjacentCoords = new Vector2(coords.x, coords.y - _blockHeight);
    if (indexes.y > 0 && !blocksCoords.ContainsKey(adjacentCoords.x + "_" + adjacentCoords.y))
    {
      CreateLayoutBlock(indexes, adjacentCoords, _bgo.transform, skin, "front");
    }

    // Left
    adjacentCoords = new Vector2(coords.x - _blockWidth, coords.y);
    if (indexes.x > 0 && !blocksCoords.ContainsKey(adjacentCoords.x + "_" + adjacentCoords.y))
    {
      CreateLayoutBlock(indexes, adjacentCoords, _bgo.transform, skin, "left");
    }

    // Right
    adjacentCoords = new Vector2(coords.x + _blockWidth, coords.y);
    if (indexes.x < GetMaxLanes - 1 && !blocksCoords.ContainsKey(adjacentCoords.x + "_" + adjacentCoords.y))
    {
      CreateLayoutBlock(indexes, adjacentCoords, _bgo.transform, skin, "right");
    }

    // Street Light
    if ((indexes.x == 0 || indexes.x == GetValidPositions.Length - 1) && (skin.StreetLightAsset != null && _streetLightFrequency > 0))
    {
      if (indexes.y == _nextStreetLight - 1)
      {
        CreateStreetLight(indexes, coords, _bgo.transform, skin);
      }
    }
  }

  private void CreateLayoutBlock(Vector2 indexes, Vector2 coords, Transform appendTo, StageSkin skin, string direction = "none")
  {
    string meshCase;
    bool flipped = false;

    if (direction == "left" || direction == "right")
    {
      meshCase = "ledge";

      if (direction == "left")
        flipped = true;
    }
    else
    {
      if (indexes.x > 0 && indexes.x < GetMaxLanes - 1)
        meshCase = "mid";
      else
      {
        meshCase = "side";

        if (indexes.x == GetMaxLanes - 1)
          flipped = true;
      }

      if (direction == "back" || direction == "front")
        meshCase += "_" + direction;
    }

    if (meshCase == "mid" || meshCase == "side" || meshCase == "ledge")
    {
      if (indexes.y != 0 && indexes.y % 2 != 0)
        meshCase += "_alt";
    }

    InstantiateSkinAsset(meshCase, flipped, coords, appendTo, skin);
  }

  private void CreateStreetLight(Vector2 indexes, Vector2 coords, Transform appendTo, StageSkin skin)
  {
    bool flipped = true;
    if (indexes.x == 0)
      flipped = false;

    InstantiateSkinAsset("street_light", flipped, coords, appendTo, skin);
  }

  private void InstantiateSkinAsset(string meshCase, bool flipped, Vector2 coords, Transform appendTo, StageSkin skin)
  {
    _basset = Instantiate(skin.GetAsset(meshCase)) as GameObject;
    _basset.name = "mesh: " + meshCase;
    if (flipped)
      _basset.name += " (flipped)";
    _brenderer = _basset.GetComponent<Renderer>();
    if (meshCase != "street_light")
      _brenderer.material = skin.SkinMaterial;
    _basset.transform.position = new Vector3(coords.x, 0f, coords.y);
    _basset.transform.parent = appendTo;

    Vector3 scale = _basset.transform.localScale;
    if (flipped)
    {
      scale.x = -scale.x;
    }
    scale.x = (float)Math.Round(scale.x, 3);
    scale.y = (float)Math.Round(scale.y, 3);
    scale.z = (float)Math.Round(scale.z, 3);
    _basset.transform.localScale = scale;
  }

  public void DestroyAllBlocks()
  {
    gameObject.SetActive(true);

    _blocks = GetTotalBlocks();

    for (int i = 0; i < _blocks.Length; i++)
    {
      DestroyImmediate(_blocks[i]);
    }
  }
}