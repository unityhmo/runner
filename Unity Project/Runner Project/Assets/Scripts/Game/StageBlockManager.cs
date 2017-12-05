using UnityEngine;
using System.Collections.Generic;

public class StageBlockManager : MonoBehaviour
{
  [SerializeField]
  private GameObject _blockPrefab;
  [SerializeField]
  private GameObject _stageSkin;
  private int _minLanes = 3;
  [SerializeField]
  private int _maxLanes; // Only odd numbers please
  [SerializeField]
  private int _createRows;
  private List<float> _validPositions;
  private GameObject[] _blocks;
  private float _blockWidth;
  private float _blockHeight;
  private bool _emptyZ = true;
  private float _lowestZ;
  private float _highestZ;
  private int _stageLength;

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
      blockPosition.x = laneJump * _blockWidth;
      blockPosition.z = currentRow * _blockHeight;

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

    _validPositions = new List<float>();
    for (int i = 0; i < _maxLanes; i++)
    {
      xPosition = laneJump * _blockWidth;
      _validPositions.Add(xPosition);

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
  }

  public void CheckValidPositions(bool andRemove = false)
  {
    StageBlock blockComponent;
    _blocks = GameObject.FindGameObjectsWithTag(BaseValues.TAG_STAGE_BLOCK);

    foreach (GameObject block in _blocks)
    {
      blockComponent = block.GetComponent<StageBlock>();
      blockComponent.CheckValidPosition();

      if (andRemove && !blockComponent.IsValid)
      {
        DestroyImmediate(block);
      }
    }
  }

  public List<float> GetValidPositions
  {
    get
    {
      return _validPositions;
    }
  }

  public void StageCleanup()
  {
    _emptyZ = true;

    // First we remove all invalid blocks
    CheckValidPositions(true);

    // Now we removed overlapped blocks
    RemoveOverlapped();
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
      _blockWidth = renderer.bounds.extents.x * 2f;
      _blockHeight = renderer.bounds.extents.z * 2f;
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

  public int GetTotalBlocks()
  {
    _blocks = GameObject.FindGameObjectsWithTag(BaseValues.TAG_STAGE_BLOCK);

    return _blocks.Length;
  }

  public int SetStageLength()
  {
    _stageLength = 0;

    if (GetTotalBlocks() > 0)
    {
      _stageLength = (int)Mathf.Round((GetHighestZ - GetLowestZ)/ GetBlockHeight) + 1;
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

  private void RemoveOverlapped()
  {
    Dictionary<string, GameObject> blocksCoords = new Dictionary<string, GameObject>();

    GameObject temp;
    Vector3 pos;
    string label;

    _blocks = GameObject.FindGameObjectsWithTag(BaseValues.TAG_STAGE_BLOCK);

    foreach (GameObject block in _blocks)
    {
      pos = block.transform.position;
      label = pos.x + "_" + pos.z;
      temp = null;

      if (blocksCoords.TryGetValue(label, out temp))
        DestroyImmediate(block);
      else
        blocksCoords.Add(label, block);
    }
  }

  public void ConstructFinalStage()
  {
    StageCleanup();

    StageSkin skin = _stageSkin.GetComponent<StageSkin>();

    skin.GetAsset("test");
  }
}
