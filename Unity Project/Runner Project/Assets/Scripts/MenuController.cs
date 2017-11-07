using UnityEngine;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
  private GameMaster master;

  [SerializeField]
  private GameObject canvasMain;
  [SerializeField]
  private GameObject canvasPlay;
  [SerializeField]
  private GameObject canvasOptions;
  [SerializeField]
  private GameObject canvasCredits;

  [SerializeField]
  private GameObject baseStageButton;

  // Use this for initialization
  void Start()
  {
    master = GameMaster.getInstance();
    createStageButtons();
  }

  public void playButtonHandler()
  {
    canvasMain.SetActive(false);
    canvasPlay.SetActive(true);
  }

  public void optionsButtonHandler()
  {
    canvasMain.SetActive(false);
    canvasOptions.SetActive(true);
  }

  public void creditsButtonHandler()
  {
    canvasMain.SetActive(false);
    canvasCredits.SetActive(true);
  }

  public void selectStageButtonHandler(int stageIndex)
  {
    master.setStageIndex(stageIndex);
    master.goToScene(2);
  }

  public void backButtonHandler()
  {
    canvasMain.SetActive(true);
    canvasPlay.SetActive(false);
    canvasOptions.SetActive(false);
    canvasCredits.SetActive(false);
  }

  public void exitButtonHandler()
  {
    Debug.Log("Quit");
    Application.Quit();
  }

  private void createStageButtons()
  {
    List<Stage> stages = master.getStages();
    GameObject newStageItem;
    Vector3 position = new Vector3(baseStageButton.transform.position.x, baseStageButton.transform.position.y, 0);

    for (int i = 0; i < stages.Count; i++)
    {
      if (!stages[i].getIslocked())
      {
        newStageItem = Instantiate (baseStageButton, canvasPlay.transform, false);
		newStageItem.transform.localPosition = new Vector3 (-260, 226, 1738); // TODO - hardcoded values, replace for relative points from prefab.
        newStageItem.name = "s" + i;

		newStageItem.GetComponent<StageMenuItem> ()
			.setLabel (stages [i].getLabel ())
			.setStars (stages [i].getStars ())
			.setLetters (stages [i].getLetters ())
			.setStageIndex (i).setParent (this);
      }
    }
  }
}
