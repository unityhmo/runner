using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameDataController
{
  // All Game Info is saved in this serialized object
  public GameDataInfo dataInfo = new GameDataInfo();

  private string savePathName = Application.persistentDataPath + "/gameInfo.dat";


  public void Save()
  {
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(savePathName);

    bf.Serialize(file, dataInfo);
    file.Close();
  }

  public void Load()
  {
    if (File.Exists(savePathName))
    {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(savePathName, FileMode.Open);
      try
      {
        // Reading File and setting value to data object
        dataInfo = (GameDataInfo)bf.Deserialize(file);
      }
      catch
      {
        // If serialization fails, the saved object is old or damage. Create default one.
        file.Close();
        createAndSaveDefault();
      }
      file.Close();

    }
    else
    {
      createAndSaveDefault();
    }
  }

  public void saveUnlockedStage(int stageIndex)
  {
    dataInfo.statesLocked["level_" + stageIndex] = false;
    Save();
    Debug.Log("Level " + stageIndex + " Unlocked!");
  }

  public void setAudioSetting(bool value)
  {
    dataInfo.audio_enabled = value;
    Save();
    Debug.Log("Audio Set to " + value);
  }

  private void createAndSaveDefault()
  {
    dataInfo = new GameDataInfo();
    setDefault();
    Save();
  }

  private void setDefault()
  {
    // User Preferences
    dataInfo.audio_enabled = true;

    // User Game Info
    dataInfo.statesLocked.Add("level_0", false);
    dataInfo.statesLocked.Add("level_1", true);
    dataInfo.statesLocked.Add("level_2", true);
  }

}
