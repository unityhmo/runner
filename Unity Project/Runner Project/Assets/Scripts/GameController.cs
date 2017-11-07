using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
  private GameMaster master;
  private int currentStageIndex;
  [SerializeField]
  private int getReadyTimer = 3;
  [SerializeField]
  private int hp = 3;
  private int maxHP;
  private int score;
  [SerializeField]
  private GameObject CanvasWin;
  [SerializeField]
  private GameObject CanvasLose;
  [SerializeField]
  private Text txtHP;
  [SerializeField]
  private Text txtScore;
  [SerializeField]
  private GameObject nextStageButton;
  [SerializeField]
  private Transform scenarioHolder;

  private Stage stageData;

  void Start()
  {
    master = GameMaster.getInstance();
    currentStageIndex = master.getStageIndex();
    stageData = master.getStage(currentStageIndex);

    loadStage(stageData.getAssetPath());

    maxHP = hp;
    updateUI();

    StartCoroutine(startGame());
  }

  private void loadStage(string path)
  {
    GameObject stageAssets = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
    stageAssets.name = "stage_" + currentStageIndex + ": " + stageData.getLabel();
    stageAssets.transform.parent = scenarioHolder;
  }

  public void fadeOutFinished()
  {
    //StartCoroutine(startGame());
  }

  public void pickUpPowerUp()
  {
    score++;
    updateUI();
  }

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

  public int getCurrentHP()
  {
    return hp;
  }

  public void win()
  {
    if (score > stageData.getStars())
    {
      // TODO: This is wrong, Stars are meassured from all pickups, then divided by mathf.floor 3
      // Only as a place holder and to see changes in menu
      stageData.setStars(score);
    }
    master.setStage(currentStageIndex, stageData);
    showWinUI();

    // Unlooks next stage
    int nextStageIndex = currentStageIndex + 1;
    if (nextStageIndex < master.getStageCount())
    {
      Stage nextStage = master.getStage(nextStageIndex);
      nextStage.setIslocked(false);
      master.setStage(nextStageIndex, nextStage);
    }
    else
    {
      Destroy(nextStageButton);
    }
  }

  public void lose()
  {
    showGameOverUI();
  }

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
    master.setStageIndex(currentStageIndex);
    master.goToScene(2);
  }

  public void goToMenu()
  {
    master.goToScene(1);
  }

  private void showWinUI()
  {
    CanvasWin.SetActive(true);
  }

  private void showGameOverUI()
  {
    CanvasLose.SetActive(true);
  }

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
