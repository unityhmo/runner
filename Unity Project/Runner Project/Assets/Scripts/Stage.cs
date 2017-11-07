public class Stage
{
  private bool isLocked = true;
  private string label = "noname";
  private string assetPath;
  private int totalPowerUps = 0;
  private int highestPowerUps = 0;
  private int stars = 0;
  private int letters = 0;

  public Stage setIslocked(bool val)
  {
    isLocked = val;
    return this;
  }
  public bool getIslocked()
  {
    return isLocked;
  }

  public Stage setLabel(string val)
  {
    label = val;
    return this;
  }
  public string getLabel()
  {
    return label;
  }

  public Stage setAssetPath(string val)
  {
    assetPath = val;
    return this;
  }
  public string getAssetPath()
  {
    return assetPath;
  }

  public Stage setTotalPowerUps(int val)
  {
    totalPowerUps = val;
    return this;
  }
  public int getTotalPowerUps()
  {
    return totalPowerUps;
  }
  public Stage setHighestPowerUps(int val)
  {
    highestPowerUps = val;
    return this;
  }
  public int getHighestPowerUps()
  {
    return highestPowerUps;
  }

  public Stage setStars(int val)
  {
    stars = val;
    return this;
  }
  public int getStars()
  {
    return stars;
  }

  public Stage setLetters(int val)
  {
    letters = val;
    return this;
  }
  public int getLetters()
  {
    return letters;
  }
}
