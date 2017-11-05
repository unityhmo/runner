using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
  private GameMaster master;

  [SerializeField]
  private int getReadyTimer = 3;
  [SerializeField]
  private int hp = 3;
  private int maxHP;
  private int score;

  public GameObject CanvasWin;
  public GameObject CanvasLose;
  public Text txtHP;
  public Text txtScore;

  void Start()
  {
    master = GameMaster.getInstance();

    maxHP = hp;
    updateUI();

    StartCoroutine(startGame());
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
    showWinUI();
  }

  public void lose()
  {
    showGameOverUI();
  }

  public void restartScene()
  {
    master.goToScene("game");
  }

  public void goToMenu()
  {
    master.goToScene("menu");
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
    Debug.Log("Wait for " + getReadyTimer + " seconds...");

    yield return new WaitForSeconds(getReadyTimer);

    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().startGame();
  }
}
