using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * Game Scene MAIN GameObject component.
 * In charge of manipulating and centralizing data from game scene.
 * Whatever info has to persist out of this scene, this component is in charge of letting GameMaster handle the rest (and no one else!).This is the only door from inside and outside of this scene.
 */
public class GameController : MonoBehaviour
{
  private GameMaster _master;
  private int _currentStageIndex;
  // Instead of start running right away, we have a delayer (for animation or whatever)
  [SerializeField]
  private int _getReadyTimer = 3;
  [SerializeField]
  private int _hp = 3; // Initial health points at stage
  private int _orbCounter; // Pickup counter
  private int _totalOrbs;
  // UI Components
  [SerializeField]
  private GameObject _canvasWin;
  [SerializeField]
  private GameObject _canvasLose;
  [SerializeField]
  private GameObject[] heartUi;
  [SerializeField]
  private GameObject[] winStarUi;
  [SerializeField]
  private Image gearProgresion;
  [SerializeField]
  private Text _txtPickUps;
  [SerializeField]
  private GameObject _nextStageButton;
  [SerializeField]
  private Text _stageName;
  [SerializeField]
  private Transform _scenarioHolder;
  private Stage _stageData;
  [SerializeField]
  private AudioClip _victoryIntro;
  [SerializeField]
  private AudioClip _victoryLoop;

  void Start()
  {
    _master = GameMaster.GetInstance();

    _currentStageIndex = _master.GetStageIndex();
    _stageData = _master.GetStage(_currentStageIndex);
    _stageName.text = _stageData.Label;
    loadStage(_stageData.AssetPath);

    UpdateUI();

    // Starts up delayer at beginning of stage
    StartCoroutine(StartGame());
  }

  // Right at the start, we bring from Resource folder the corresponding stage level we want. This data is provided from GameMaster, it knows everything!
  // Anyways, we instantiate it and move it as a child of Scenario gameobject, it is the origin point for stages.
  private void loadStage(string path)
  {
    GameObject stageAssets = Instantiate(Resources.Load(BaseValues.PATH_STAGES_RESOURCES + path, typeof(GameObject))) as GameObject;
    stageAssets.name = "stage_" + _currentStageIndex + ": " + _stageData.Label;
    stageAssets.transform.parent = _scenarioHolder;
  }

  // This is triggered after fadeout is done, we could use this further on...
  public void FadeOutFinished()
  {
    //StartCoroutine(startGame());
  }

  // Collision detected from pick up. Here we update score value and refresh UI
  public void PickUpOrb()
  {
    AudioManager.GetUIFX().PickUpOrb();
    _orbCounter++;
    UpdateUI();
  }
  public void OrbDetected()
  {
    _totalOrbs++;
  }

  // Collision detected from obstacle or falling to death. We update health and refresh UI
  public void RegisterDamage(int damage, bool instaDeath = false)
  {
    if (instaDeath)
      _hp = 0;
    else
      _hp -= damage;

    if (_hp == 0)
      Lose();

    UpdateUI();
  }

  // HP getter for other components in this scene. GameController is the boss after all (right under GameMaster of course!).
  public int GetCurrentHP()
  {
    return _hp;
  }

  // Upon winning, we tell GameMaster to update our current stage data, and unlock the next stage (if there is one)
  // We take care of showing winner's UI
  public void Win()
  {
    if (_orbCounter > _stageData.HighestPickUps)
    {
      // TODO: This is wrong, Stars are meassured from all pickups, then divided by mathf.floor 3
      // Only as a place holder and to see changes in menu
      _stageData.SetHighestPickUps(_orbCounter);
      _stageData.SetTotalPickUps(_totalOrbs);
      Debug.Log("New highscore!");
    }

    _master.SetStage(_currentStageIndex, _stageData);
    ShowWinUI();

    // Unlooks next stage
    int nextStageIndex = _currentStageIndex + 1;
    if (nextStageIndex < _master.GetStageCount())
    {
      Stage nextStage = _master.GetStage(nextStageIndex);
      nextStage.SetIslocked(false);
      _master.SetStage(nextStageIndex, nextStage);
    }
    else
    {
      Destroy(_nextStageButton);
    }

    AudioManager.GetMFX().SetClip(_victoryLoop, _victoryIntro);
  }

  // Shows loser's UI
  public void Lose()
  {
    ShowGameOverUI();
  }

  // Public function for UI Buttons----INI
  public void NextStage()
  {
    AudioManager.GetUIFX().Accept();
    _currentStageIndex++;
    LoadScene();
  }
  public void RetryStage()
  {
    AudioManager.GetUIFX().Accept();
    LoadScene();
  }
  private void LoadScene()
  {
    _master.SetStageIndex(_currentStageIndex);
    _master.GoToScene(2);
  }

  public void GoToMenu()
  {
    AudioManager.GetUIFX().Accept();
    _master.GoToScene(1);
  }
  // Public function for UI Buttons----END

  private void ShowWinUI()
  {
        winStarUi[0].SetActive(((float)_stageData.HighestPickUps / (float)_totalOrbs) * 100 >= BaseValues.OneStar);
        winStarUi[1].SetActive(((float)_stageData.HighestPickUps / (float)_totalOrbs) * 100 >= BaseValues.TwoStar);
        winStarUi[2].SetActive(((float)_stageData.HighestPickUps / (float)_totalOrbs) * 100 >= BaseValues.ThreeStar);
        _canvasWin.SetActive(true);
  }

  private void ShowGameOverUI()
  {
    _canvasLose.SetActive(true);
  }

    // UI components updater
    private void UpdateUI()
    {
        _txtPickUps.text = _orbCounter.ToString() + "/" + _totalOrbs.ToString();
        //hearts
        this.heartUi[0].SetActive(_hp > 0);
        this.heartUi[1].SetActive(_hp > 1);
        this.heartUi[2].SetActive(_hp > 2);
        this.gearProgresion.fillAmount = (float)_orbCounter / (float)_totalOrbs;
    }

  IEnumerator StartGame()
  {
    // TODO: Prepare a "Ready!" message, then start
    Debug.Log("STAGE: " + _currentStageIndex + " - Wait for " + _getReadyTimer + " seconds...");

    yield return new WaitForSeconds(_getReadyTimer);

    GameObject.FindGameObjectWithTag(BaseValues.TAG_PLAYER).GetComponent<PlayerController>().StartGame();
  }
}
