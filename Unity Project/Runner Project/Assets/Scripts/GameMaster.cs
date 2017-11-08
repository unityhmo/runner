using UnityEngine;
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
  private SceneLoader sceneLoader;

  // Level to be loaded from Resources
  private int currentStageIndex = 0;
  private List<Stage> stages;

  // We make sure this object do not vanishes in loadscenes
  // If our protected static instance variable isn't ready we destroy this one and return the existing one.
  void Awake()
  {
    DontDestroyOnLoad(transform.gameObject);
    if (_instance != null)
      DestroyObject(gameObject);
    else
      _instance = this;

    sceneLoader = transform.GetComponent<SceneLoader>();
    stages = Stages.buildStages();

    // TODO: EXTERNAL LOAD - Overload local saved date here
  }

  // Build our base info for Stages,  this is before overwritting with user saved local progress data
  public List<Stage> getStages()
  {
    return stages;
  }

  // Simple getter for stage counter
  public int getStageCount()
  {
    return stages.Count;
  }

  // Returns a specific Stage data to whomever ask for it
  public Stage getStage(int stageIndex)
  {
    return stages[stageIndex];
  }
  // Overwrite specific Stage data into our stages data holder.
  // This is likely used after clearing a stage or beating a highscore
  public void setStage(int stageIndex, Stage newStageData)
  {
    stages[stageIndex] = newStageData;

    // TODO: EXTERNAL WRITE - To save file
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

  // When asked for GameMaster instance, we create one or return existing one
  public static GameMaster getInstance()
  {
    if (_instance == null)
      _instance = ObjectFactory.createGameMaster();

    return _instance;
  }
}
