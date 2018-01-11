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
  private SceneLoader _sceneLoader;
  private GameDataController _dataController;

  // Level to be loaded from Resources
  private int _currentStageIndex = 0;
  private List<Stage> _stages;

  // Stage Names. This Array defines also how many stages exists.
  private string[] _stageNames = {
      "Hello world",
      "The short path",
      "Not so short",
      "An easy one",
      "Don't jump!",
      "Long and fast",
      "Zig zag",
      "One long line",
      "Run free",
      "Impossible",
      "It's so simple"
  };

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
    _dataController.Load(_stageNames);
    _stages = Stages.BuildStages(_stageNames, _dataController.GetDataInfo());

    SetupSoundAndMusicManager();
  }

  public void RefreshStages()
  {
    _stages = Stages.BuildStages(_stageNames, _dataController.GetDataInfo());
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

    _dataController.SaveUnlockedStage(stageIndex, newStageData);
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

  private void SetupSoundAndMusicManager()
  {
    AudioManager.SetUIFX(CreateAudioObject(BaseValues.LABEL_UI_AUDIO).GetComponent<AudioUserInterface>());
    AudioManager.SetMFX(CreateAudioObject(BaseValues.LABEL_MUSIC_AUDIO).GetComponent<AudioMusic>());
  }

  private GameObject CreateAudioObject(string name)
  {
    GameObject go;
    go = Instantiate(Resources.Load(BaseValues.PATH_AUDIO_RESOURCES + name, typeof(GameObject))) as GameObject;
    go.name = name;
    go.transform.parent = transform;

    return go;
  }
}