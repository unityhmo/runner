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
  private Text _txtStarsNumberLabel;
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
    string starsLabel = "";
    while (starsNumber-- > 0)
    {
      starsLabel += STAR_ICON;
    }
    _txtStarsNumberLabel.text = starsLabel;
    return this;
  }

  private void ButtonHandler()
  {
    _menuController.SelectStageButtonHandler(_stageIndex);
  }
}
