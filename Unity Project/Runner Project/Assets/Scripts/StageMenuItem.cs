using UnityEngine;
using UnityEngine.UI;

/*
 * Basic stage button behavior component.
 * Here we have setters of values so we can create lists of buttons for Stage Selection menu
 */
public class StageMenuItem : MonoBehaviour
{
  [SerializeField] private Button _buttonStage;
  [SerializeField] private Text _txtStageIndex;
  [SerializeField] private Text _txtStageLabel;
  [SerializeField] private Text _txtStars;
  [SerializeField] private Text _txtLetters;

  private int _stageIndex = 0;
  private MenuController _parent;

  public StageMenuItem SetStageIndex(int val)
  {
    _stageIndex = val;
    _txtStageIndex.text += (val + 1).ToString();
    return this;
  }

  public StageMenuItem SetLabel(string val)
  {
    _txtStageLabel.text = val.ToString();
    return this;
  }

  public StageMenuItem SetStars(int val)
  {
    _txtStars.text = val.ToString();
    return this;
  }

  public StageMenuItem SetLetters(int val)
  {
    _txtLetters.text = val.ToString();
    return this;
  }

  public StageMenuItem SetParent(MenuController val)
  {
    _parent = val;
    _buttonStage.onClick.AddListener(ButtonHandler);
    return this;
  }

  private void ButtonHandler()
  {
    _parent.SelectStageButtonHandler(_stageIndex);
  }
}
