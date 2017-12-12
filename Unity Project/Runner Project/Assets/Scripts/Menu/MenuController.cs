using UnityEngine;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
  private GameMaster _master;

  [SerializeField]
  private GameObject _panelMain;
  [SerializeField]
  private GameObject _panelPlay;
  [SerializeField]
  private GameObject _panelOptions;
  [SerializeField]
  private GameObject _panelCredits;
  [SerializeField]
  private GameObject _buttonBack;
  [SerializeField]
  private Skybox _mySkybox;
  [SerializeField]
  private Material[] _skyboxPool; // expected to have 4 items
  [SerializeField]
  private Animator _hmoManAnimatorController;
  [SerializeField]
  private Animator _cameraAnimatorController;
  [SerializeField]
  private GameObject _baseStageButton;
  [SerializeField]
  private GameObject _stageContainer;
  [SerializeField]
  private AudioClip _mainMenuThemeIntro;
  [SerializeField]
  private AudioClip _mainMenuThemeLoop;

  void Start()
  {
    _master = GameMaster.GetInstance();
    CreateStageButtons();
    ResetPanels();

    AudioManager.GetMFX().SetClip(_mainMenuThemeLoop, _mainMenuThemeIntro);
  }

  public void PlayButtonHandler()
  {
    AudioManager.GetUIFX().Accept();
    _panelMain.SetActive(false);
    _panelPlay.SetActive(true);
    _buttonBack.SetActive(true);
    _mySkybox.material = _skyboxPool[2];
    _hmoManAnimatorController.SetBool("isReady", true);
    _cameraAnimatorController.SetBool("isPlay", true);
  }

  public void OptionsButtonHandler()
  {
    AudioManager.GetUIFX().Accept();
    _panelMain.SetActive(false);
    _panelOptions.SetActive(true);
    _buttonBack.SetActive(true);
    _mySkybox.material = _skyboxPool[0];
    _hmoManAnimatorController.SetBool("isWaving", true);
  }

  public void CreditsButtonHandler()
  {
    AudioManager.GetUIFX().Accept();
    _panelMain.SetActive(false);
    _panelCredits.SetActive(true);
    _buttonBack.SetActive(true);
    _mySkybox.material = _skyboxPool[1];
    _hmoManAnimatorController.SetBool("isWaving", true);
    _cameraAnimatorController.SetBool("isCredits", true);
  }

  public void SelectStageButtonHandler(int stageIndex)
  {
    AudioManager.GetUIFX().Accept();
    _master.SetStageIndex(stageIndex);
    _master.GoToScene(2);
  }

  public void ExitButtonHandler()
  {
    AudioManager.GetUIFX().Accept();
    Application.Quit();
  }

  public void ResetPanels(bool isButton = false)
  {
    if (isButton)
    {
      AudioManager.GetUIFX().Cancel();
    }

    _panelMain.SetActive(true);
    _panelPlay.SetActive(false);
    _panelOptions.SetActive(false);
    _panelCredits.SetActive(false);
    _buttonBack.SetActive(false);
    _mySkybox.material = _skyboxPool[3];
    _hmoManAnimatorController.SetBool("isReady", false);
    _hmoManAnimatorController.SetBool("isWaving", false);
    _cameraAnimatorController.SetBool("isCredits", false);
    _cameraAnimatorController.SetBool("isPlay", false);

  }

  private void CreateStageButtons()
  {
    List<Stage> stages = _master.GetStages();
    GameObject newStageItem;

    for (int i = 0; i < stages.Count; i++)
    {
      if (!stages[i].Islocked)
      {
        newStageItem = Instantiate(_baseStageButton, _stageContainer.transform, false);
        newStageItem.transform.localPosition = new Vector3(0, 0, 0); // TODO - hardcoded values, replace for relative points from prefab.
        newStageItem.name = "s" + i;

        newStageItem.GetComponent<StageMenuItem>()
            .SetStageNameLabel(stages[i].Label, false)
            .SetStars(BaseValues.GetStars(stages[i].HighestPickUps, stages[i].TotalPickUps))
            .SetStageIndex(i);
      }
    }
  }

  public void ToggleSoundButtonHandler(bool value)
  {
    Debug.Log("toggle buton"+value);
    AudioListener.volume = value?1:0;
    _master.GetDataController().SetAudioSetting(value);
  }
}
