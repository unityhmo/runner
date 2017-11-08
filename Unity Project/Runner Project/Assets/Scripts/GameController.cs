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
  private int currentStageIndex;

  // Instead of start running right away, we have a delayer (for animation or whatever)
  [SerializeField] private int getReadyTimer = 3;

  [SerializeField] private int hp = 3; // Initial health points at stage
  private int maxHP;

  private int score; // Power up counter

  // UI Components
  [SerializeField] private GameObject CanvasWin;
  [SerializeField] private GameObject CanvasLose;
  [SerializeField] private Text txtHP;
  [SerializeField] private Text txtScore;
  [SerializeField] private GameObject nextStageButton;

  // Transform that will work as origin for all Resources loaded at the beginning of each stage.
  [SerializeField] private Transform scenarioHolder;

  private Stage stageData;

  void Start()
  {
    _master = GameMaster.getInstance();

    currentStageIndex = _master.getStageIndex();
    stageData = _master.getStage(currentStageIndex);

    loadStage(stageData.getAssetPath());

    maxHP = hp;
    updateUI();

    // Starts up delayer at beginning of stage
    StartCoroutine(startGame());
  }

  // Right at the start, we bring from Resource folder the corresponding stage level we want. This data is provided from GameMaster, it knows everything!
  // Anyways, we instantiate it and move it as a child of Scenario gameobject, it is the origin point for stages.
  private void loadStage(string path)
  {
    GameObject stageAssets = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
    stageAssets.name = "stage_" + currentStageIndex + ": " + stageData.getLabel();
    stageAssets.transform.parent = scenarioHolder;
  }

  // This is triggered after fadeout is done, we could use this further on...
  public void fadeOutFinished()
  {
    //StartCoroutine(startGame());
  }

  // Collision detected from power up. Here we update score value and refresh UI
  public void pickUpPowerUp()
  {
    score++;
    updateUI();
  }

  // Collision detected from obstacle or falling to death. We update health and refresh UI
  public void registerDamage(bool instaDeath = false)
  {
    if (instaDeath)
      hp = 0;
    else
      hp--;

    if (hp == 0)
      lose();

    updateUI();
  }

  // HP getter for other components in this scene. GameController is the boss after all (right under GameMaster of course!).
  public int getCurrentHP()
  {
    return hp;
  }

  // Upon winning, we tell GameMaster to update our current stage data, and unlock the next stage (if there is one)
  // We take care of showing winner's UI
  public void win()
  {
    if (score > stageData.getStars())
    {
      // TODO: This is wrong, Stars are meassured from all pickups, then divided by mathf.floor 3
      // Only as a place holder and to see changes in menu
      stageData.setStars(score);
    }

    _master.setStage(currentStageIndex, stageData);
    showWinUI();

    // Unlooks next stage
    int nextStageIndex = currentStageIndex + 1;
    if (nextStageIndex < _master.getStageCount())
    {
      Stage nextStage = _master.getStage(nextStageIndex);
      nextStage.setIslocked(false);
      _master.setStage(nextStageIndex, nextStage);
    }
    else
    {
      Destroy(nextStageButton);
    }
  }

  // Shows loser's UI
  public void lose()
  {
    showGameOverUI();
  }

  // Public function for UI Buttons----INI
  public void nextStage()
  {
    currentStageIndex++;
    loadScene();
  }
  public void retryStage()
  {
    loadScene();
  }
  private void loadScene()
  {
    _master.setStageIndex(currentStageIndex);
    _master.goToScene(2);
  }

  public void goToMenu()
  {
    _master.goToScene(1);
  }
  // Public function for UI Buttons----END

  private void showWinUI()
  {
    CanvasWin.SetActive(true);
  }

  private void showGameOverUI()
  {
    CanvasLose.SetActive(true);
  }

  // UI components updater
  private void updateUI()
  {
    txtScore.text = score.ToString();
    txtHP.text = hp.ToString() + "/" + maxHP.ToString(); ;
  }

  IEnumerator startGame()
  {
    // TODO: Prepare a "Ready!" message, then start
    Debug.Log("STAGE: " + currentStageIndex + " - Wait for " + getReadyTimer + " seconds...");

    yield return new WaitForSeconds(getReadyTimer);

    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().startGame();
  }
}
