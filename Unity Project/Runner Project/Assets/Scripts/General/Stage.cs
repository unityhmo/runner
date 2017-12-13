using System.Collections;

// Base class for each stage. Only setter and getters.
public class Stage
{
  private bool _isLocked = true;
  private string _label = "noname";
  private string _assetPath;
  private int _totalPickUps = 0;
  private int _highestPickUps = 0;

  public Stage SetConfig(int index, Hashtable stageConfig)
  {
    _isLocked = (bool)stageConfig["locked_" + index];
    _totalPickUps = (int)stageConfig["pickups_total_" + index];
    _highestPickUps = (int)stageConfig["pickups_highest_" + index];
    return this;
  }
  public Stage SetIslocked(bool val)
  {
    _isLocked = val;
    return this;
  }
  public bool Islocked
  {
    get
    {
      return _isLocked;
    }
  }
  public Stage SetLabel(string val)
  {
    _label = val;
    return this;
  }
  public string Label
  {
    get
    {
      return _label;
    }
  }

  public Stage SetAssetPath(string val)
  {
    _assetPath = val;
    return this;
  }
  public string AssetPath
  {
    get
    {
      return _assetPath;
    }
  }

  public Stage SetTotalPickUps(int val)
  {
    _totalPickUps = val;
    return this;
  }
  public int TotalPickUps
  {
    get
    {
      return _totalPickUps;
    }
  }

  public Stage SetHighestPickUps(int val)
  {
    _highestPickUps = val;
    return this;
  }
  public int HighestPickUps
  {
    get
    {
      return _highestPickUps;
    }
  }
}
