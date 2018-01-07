using UnityEngine;
using UnityEngine.UI;

/*
 * Basic stage button behavior component.
 * Here we have setters of values so we can create lists of buttons for Stage Selection menu
 */
public class StageMenuItem : MonoBehaviour
{
  const string STAR_ICON = "★";

  [SerializeField]
  private Button _buttonStage;
  [SerializeField]
  private Text _txtStageNameLabel;
  [SerializeField]
  private GameObject[] stars;
  [SerializeField]
  private MenuController _menuController;

  private int _stageIndex = 0;
  private Transform _parent;

  void Start()
  {
    _menuController = GameObject.FindGameObjectWithTag(BaseValues.TAG_MENU_CONTROLLER).GetComponent<MenuController>();
    _buttonStage.onClick.AddListener(ButtonHandler);
  }

  public StageMenuItem SetStageIndex(int val)
  {
    _stageIndex = val;
    return this;
  }

  public StageMenuItem SetStageNameLabel(string stageName, bool includeIndex)
  {
    _txtStageNameLabel.text = stageName;
    return this;
  }

  public StageMenuItem SetStars(int starsNumber)
  {
    stars[0].SetActive(starsNumber > 0);stars[0].SetActive(starsNumber > 0);
    stars[1].SetActive(starsNumber > 1);
    stars[2].SetActive(starsNumber > 2);
    return this;
  }

  private void ButtonHandler()
  {
    _menuController.SelectStageButtonHandler(_stageIndex);
  }
}
