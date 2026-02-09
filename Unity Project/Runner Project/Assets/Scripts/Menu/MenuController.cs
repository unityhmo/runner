using UnityEngine;
using UnityEngine.UI;
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
    private Toggle _toggleSound;
    [SerializeField]
    private GameObject _panelCredits;
    [SerializeField]
    private GameObject _buttonBack;
    [SerializeField]
    private Animator _hmoManAnimatorController;
    [SerializeField]
    private Animator _cameraAnimatorController;
    [SerializeField]
    private GameObject _baseStageButton;
    [SerializeField] private Transform _stageContainer;
    [SerializeField]
    private AudioClip _mainMenuThemeIntro;
    [SerializeField]
    private AudioClip _mainMenuThemeLoop;
    [SerializeField] private CanvasScaler _uiCanvas;

    private void Awake()
    {
        float aspectRation = (float)Screen.height / Screen.width;
        if (aspectRation > 1.5f) return;

        _uiCanvas.matchWidthOrHeight = 1f;
    }

    void Start()
    {
        _master = GameMaster.GetInstance();
        CreateStageButtons();
        ResetPanels();

        AudioManager.GetMFX().SetClip(_mainMenuThemeLoop, _mainMenuThemeIntro);
        _toggleSound.isOn = _master.GetDataController().GetDataInfo().AudioEnabled;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void PlayButtonHandler()
    {
        AudioManager.GetUIFX().Accept();
        _panelMain.SetActive(false);
        _panelPlay.SetActive(true);
        _buttonBack.SetActive(true);
        _hmoManAnimatorController.SetBool("playActive", true);
        _cameraAnimatorController.SetBool("isPlay", true);
    }

    public void OptionsButtonHandler()
    {
        AudioManager.GetUIFX().Accept();
        _panelMain.SetActive(false);
        _panelOptions.SetActive(true);
        _buttonBack.SetActive(true);
        _hmoManAnimatorController.SetBool("optionsActive", true);
        _cameraAnimatorController.SetBool("isOptions", true);
    }

    public void CreditsButtonHandler()
    {
        AudioManager.GetUIFX().Accept();
        _panelMain.SetActive(false);
        _panelCredits.SetActive(true);
        _buttonBack.SetActive(true);
        _hmoManAnimatorController.SetBool("creditsActive", true);
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
        _hmoManAnimatorController.SetBool("playActive", false);
        _hmoManAnimatorController.SetBool("optionsActive", false);
        _hmoManAnimatorController.SetBool("creditsActive", false);
        _cameraAnimatorController.SetBool("isPlay", false);
        _cameraAnimatorController.SetBool("isOptions", false);
        _cameraAnimatorController.SetBool("isCredits", false);

    }

    private void CreateStageButtons()
    {
        List<Stage> stages = _master.GetStages();
        GameObject newStageItem;

        ClearStagesButtons();

        for (int i = 0; i < stages.Count; i++)
        {
            if (stages[i].Islocked) continue;

            newStageItem = Instantiate(_baseStageButton, _stageContainer.transform, false);
            newStageItem.name = "s" + i;

            newStageItem.GetComponent<StageMenuItem>()
                .SetStageNameLabel(stages[i].Label, false)
                .SetStars(BaseValues.GetStars(stages[i].HighestPickUps, stages[i].TotalPickUps))
                .SetStageIndex(i);
        }
    }

    private void ClearStagesButtons()
    {
        foreach (Transform child in _stageContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ToggleSoundButtonHandler(bool value)
    {
        AudioListener.volume = value ? 1 : 0;
        _master.GetDataController().SetAudioSetting(value);
    }

    public void RestartButtonHandler()
    {
        _master.GetDataController().CreateAndSaveDefault();
        _master.RefreshStages();
        CreateStageButtons();
        ResetPanels();
    }

    public void UnlockLevelsButtonHandler(bool value)
    {
        _master.GetDataController().UnlockAll();
        _master.RefreshStages();
        CreateStageButtons();
        ResetPanels();
    }
}
