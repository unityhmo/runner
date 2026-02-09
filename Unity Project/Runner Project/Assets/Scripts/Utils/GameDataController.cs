using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameDataController
{
  // All Game Info is saved in this serialized object
  private GameDataInfo _dataInfo = new GameDataInfo();

  private string _savePathName = Application.persistentDataPath + "/game_save.sav";

  private string[] _stageNames;

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

  public void Load(string[] names)
  {
    _stageNames = names;

    if (File.Exists(_savePathName))
    {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(_savePathName, FileMode.Open);
      try
      {
        // Reading File and setting value to data object
        _dataInfo = (GameDataInfo)bf.Deserialize(file);

        AudioListener.volume = _dataInfo.AudioEnabled ? 1 : 0;

        if (_dataInfo.StageConfig == null)
        {
          CreateAndSaveDefault();
        }
        else
        {
          /* Check if the last stage is available, otherwise, reset game in catch exception*/
          bool lastStage = (bool)_dataInfo.StageConfig["locked_" + (_stageNames.Length - 1)];
        }

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

  public void SaveUnlockedStage(int stageIndex, Stage newStageData)
  {
    _dataInfo.StageConfig["locked_" + stageIndex] = false;
    _dataInfo.StageConfig["pickups_total_" + stageIndex] = newStageData.TotalPickUps;
    _dataInfo.StageConfig["pickups_highest_" + stageIndex] = newStageData.HighestPickUps;
    Save();
  }

  public void SetAudioSetting(bool value)
  {
    _dataInfo.AudioEnabled = value;
    Save();
  }

  public void CreateAndSaveDefault()
  {
    SetDefault();
    Save();
  }

  private void SetDefault()
  {
    bool currentAudioEnable = _dataInfo.AudioEnabled;

    _dataInfo = new GameDataInfo();

    _dataInfo.AudioEnabled = currentAudioEnable;

    for (int i = 0; i < _stageNames.Length; i++)
    {
      if(i == 0)
      {
        _dataInfo.StageConfig.Add("locked_0", false);
        _dataInfo.StageConfig.Add("pickups_total_0", 0);
        _dataInfo.StageConfig.Add("pickups_highest_0", 0);
      }
      else
      {
        _dataInfo.StageConfig.Add("locked_" + i, true);
        _dataInfo.StageConfig.Add("pickups_total_" + i, 0);
        _dataInfo.StageConfig.Add("pickups_highest_" + i, 0);
      }
    }
  }

  public void UnlockAll()
  {
    bool currentAudioEnable = _dataInfo.AudioEnabled;

    _dataInfo = new GameDataInfo();

    _dataInfo.AudioEnabled = currentAudioEnable;

    for (int i = 0; i < _stageNames.Length; i++)
    {
      _dataInfo.StageConfig.Add("locked_" + i, false);
      _dataInfo.StageConfig.Add("pickups_total_" + i, 0);
      _dataInfo.StageConfig.Add("pickups_highest_" + i, 0);
    }

    Save();

  }

}
