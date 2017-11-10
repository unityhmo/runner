using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameDataController
{
  // All Game Info is saved in this serialized object
  private GameDataInfo _dataInfo = new GameDataInfo();

  private string _savePathName = Application.persistentDataPath + "/gameInfo.dat";

  public GameDataInfo GetDataInfo()
  {
    return _dataInfo;
  }

  public void Save()
  {
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(_savePathName);

    bf.Serialize(file, _dataInfo);
    file.Close();
  }

  public void Load()
  {
    if (File.Exists(_savePathName))
    {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(_savePathName, FileMode.Open);
      try
      {
        // Reading File and setting value to data object
        _dataInfo = (GameDataInfo)bf.Deserialize(file);
      }
      catch
      {
        // If serialization fails, the saved object is old or damage. Create default one.
        file.Close();
        CreateAndSaveDefault();
      }
      file.Close();

    }
    else
    {
      CreateAndSaveDefault();
    }
  }

  public void SaveUnlockedStage(int stageIndex)
  {
    _dataInfo.StateIsLocked["level_" + stageIndex] = false;
    Save();
    Debug.Log("Level " + stageIndex + " Unlocked!");
  }

  public void SetAudioSetting(bool value)
  {
    _dataInfo.AudioEnabled = value;
    Save();
    Debug.Log("Audio Set to " + value);
  }

  private void CreateAndSaveDefault()
  {
    _dataInfo = new GameDataInfo();
    SetDefault();
    Save();
  }

  private void SetDefault()
  {
    // User Preferences
    _dataInfo.AudioEnabled = true;

    // User Game Info
    _dataInfo.StateIsLocked.Add("level_0", false);
    _dataInfo.StateIsLocked.Add("level_1", true);
    _dataInfo.StateIsLocked.Add("level_2", true);
  }

}
