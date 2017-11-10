// Base class for each stage. Only setter and getters.
public class Stage
{
  private bool _isLocked = true;
  private string _label = "noname";
  private string _assetPath;
  private int _pickUps = 0;
  private int _stars = 0;
  private int _letters = 0;

  public Stage SetIslocked(bool val)
  {
    _isLocked = val;
    return this;
  }
  public bool GetIslocked()
  {
    return _isLocked;
  }

  public Stage SetLabel(string val)
  {
    _label = val;
    return this;
  }
  public string GetLabel()
  {
    return _label;
  }

  public Stage SetAssetPath(string val)
  {
    _assetPath = val;
    return this;
  }
  public string GetAssetPath()
  {
    return _assetPath;
  }

  public Stage SetPickUps(int val)
  {
    _pickUps = val;
    return this;
  }
  public int GetPickUps()
  {
    return _pickUps;
  }

  public Stage SetStars(int val)
  {
    _stars = val;
    return this;
  }
  public int GetStars()
  {
    return _stars;
  }

  public Stage SetLetters(int val)
  {
    _letters = val;
    return this;
  }
  public int GetLetters()
  {
    return _letters;
  }
}
