using UnityEngine;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
	private GameMaster _master;

	[SerializeField] private GameObject _panelMain;
	[SerializeField] private GameObject _panelPlay;
	[SerializeField] private GameObject _panelOptions;
	[SerializeField] private GameObject _panelCredits;
	[SerializeField] private GameObject _buttonBack;
	[SerializeField] private Skybox _mySkybox;
	[SerializeField] private Material[] _skyboxPool; // expected to have 4 items
	[SerializeField] private Animator _hmoManAnimatorController;
	[SerializeField] private Animator _cameraAnimatorController;

	[SerializeField] private GameObject _baseStageButton;

	void Start ()
	{
		_master = GameMaster.getInstance ();
		createStageButtons ();
		resetPanels ();
	}

	public void playButtonHandler ()
	{
		_panelMain.SetActive (false);
		_panelPlay.SetActive (true);
		_buttonBack.SetActive (true);
		_mySkybox.material = _skyboxPool [2];
		_hmoManAnimatorController.SetBool ("isReady", true);
		_cameraAnimatorController.SetBool ("isPlay", true);
	}

	public void optionsButtonHandler ()
	{
		_panelMain.SetActive (false);
		_panelOptions.SetActive (true);
		_buttonBack.SetActive (true);
		_mySkybox.material = _skyboxPool [0];
		_hmoManAnimatorController.SetBool ("isWaving", true);
	}

	public void creditsButtonHandler ()
	{
		_panelMain.SetActive (false);
		_panelCredits.SetActive (true);
		_buttonBack.SetActive (true);
		_mySkybox.material = _skyboxPool [1];
		_hmoManAnimatorController.SetBool ("isWaving", true);
		_cameraAnimatorController.SetBool ("isCredits", true);
	}

	public void selectStageButtonHandler (int stageIndex)
	{
		_master.setStageIndex (stageIndex);
		_master.goToScene (2);
	}

	public void exitButtonHandler ()
	{
		Debug.Log ("Quit");
		Application.Quit ();
	}

	public void resetPanels ()
	{
		_panelMain.SetActive (true);
		_panelPlay.SetActive (false);
		_panelOptions.SetActive (false);
		_panelCredits.SetActive (false);
		_buttonBack.SetActive (false);
		_mySkybox.material = _skyboxPool [3];
		_hmoManAnimatorController.SetBool ("isReady", false);
		_hmoManAnimatorController.SetBool ("isWaving", false);
		_cameraAnimatorController.SetBool ("isCredits", false);
		_cameraAnimatorController.SetBool ("isPlay", false);
	}

	private void createStageButtons ()
	{
		List<Stage> stages = _master.getStages ();
		GameObject newStageItem;
		Vector3 position = new Vector3 (_baseStageButton.transform.position.x, _baseStageButton.transform.position.y, 0);

		for (int i = 0; i < stages.Count; i++) {
			if (!stages [i].getIslocked ()) {
				newStageItem = Instantiate (_baseStageButton, _panelPlay.transform, false);
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
