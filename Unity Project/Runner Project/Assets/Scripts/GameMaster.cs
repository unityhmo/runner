﻿using UnityEngine;
using System.Collections.Generic;

/*
 * GameMaster is the core of this game.
 * This guy is in charge of moving through scenes with all the game's data. Serving it to stage builders, menu builders and also retrieving/updating data from outside the app.
 * This component can only exist once throught out all scenes. It's a singleton pattern instace.
 * So, whenever we ask for GameMaster we will end up the one and only one instance.
 * This gameObject persist through scenes, it has DontDestroyOnLoad, so good luck in getting rid of it.
 */
public class GameMaster : MonoBehaviour
{
  protected static GameMaster _instance;
  private SceneLoader _sceneLoader;
  private GameDataController _dataController;

  // Level to be loaded from Resources
  private int _currentStageIndex = 0;
  private List<Stage> _stages;

  // We make sure this object do not vanishes in loadscenes
  // If our protected static instance variable isn't ready we destroy this one and return the existing one.
  void Awake()
  {

    if (_instance == null)
    {
      DontDestroyOnLoad(gameObject);
      _instance = this;
    }
    else if (_instance != this)
    {
      Destroy(gameObject);
    }

    _sceneLoader = transform.GetComponent<SceneLoader>();
    _dataController = new GameDataController();
    _dataController.Load();
    _stages = Stages.BuildStages(_dataController.GetDataInfo());

  }

  // Build our base info for Stages,  this is before overwritting with user saved local progress data
  public List<Stage> GetStages()
  {
    return _stages;
  }

  // Simple getter for stage counter
  public int GetStageCount()
  {
    return _stages.Count;
  }

  // Returns a specific Stage data to whomever ask for it
  public Stage GetStage(int stageIndex)
  {
    return _stages[stageIndex];
  }
  // Overwrite specific Stage data into our stages data holder.
  // This is likely used after clearing a stage or beating a highscore
  public void SetStage(int stageIndex, Stage newStageData)
  {
    _stages[stageIndex] = newStageData;

    _dataController.SaveUnlockedStage(stageIndex);
  }

  public void SetStageIndex(int val)
  {
    _currentStageIndex = val;
  }
  public int GetStageIndex()
  {
    return _currentStageIndex;
  }

  public void GoToScene(int sceneIndex)
  {
    _sceneLoader.GoToScene(sceneIndex);
  }
  public SceneLoader GetSceneLoader()
  {
    return _sceneLoader;
  }

  // When asked for GameMaster instance, we create one or return existing one
  public static GameMaster GetInstance()
  {
    if (_instance == null)
      _instance = ObjectFactory.CreateGameMaster();

    return _instance;
  }

  public GameDataController GetDataController()
  {
    return _dataController;
  }
}