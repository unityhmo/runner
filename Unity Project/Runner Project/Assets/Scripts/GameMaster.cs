using UnityEngine;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour
{
  protected static GameMaster _instance;
  private SceneLoader sceneLoader;

  // Level to be loaded from Resources
  private int currentStageIndex = 0;
  private List<Stage> stages;

  void Awake()
  {
    DontDestroyOnLoad(transform.gameObject);
    if (_instance != null)
      DestroyObject(gameObject);
    else
      _instance = this;

    sceneLoader = transform.GetComponent<SceneLoader>();
    stages = Stages.buildStages();

    // TODO: Overload local saved date here
  }

  public List<Stage> getStages()
  {
    return stages;
  }
  public int getStageCount()
  {
    return stages.Count;
  }
  public Stage getStage(int stageIndex)
  {
    return stages[stageIndex];
  }
  public void setStage(int stageIndex, Stage newStageData)
  {
    stages[stageIndex] = newStageData;
  }

  public void setStageIndex(int val)
  {
    currentStageIndex = val;
  }
  public int getStageIndex()
  {
    return currentStageIndex;
  }

  public void goToScene(int sceneIndex)
  {
    sceneLoader.goToScene(sceneIndex);
  }
  public SceneLoader getSceneLoader()
  {
    return sceneLoader;
  }
  public static GameMaster getInstance()
  {
    if (_instance == null)
      _instance = ObjectFactory.createGameMaster();

    return _instance;
  }
}
