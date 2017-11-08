using UnityEngine;
using UnityEngine.UI;

/*
 * Basic stage button behavior component.
 * Here we have setters of values so we can create lists of buttons for Stage Selection menu
 */
public class StageMenuItem : MonoBehaviour
{
  [SerializeField] private Button buttonStage;
  [SerializeField] private Text txtStageIndex;
  [SerializeField] private Text txtStageLabel;
  [SerializeField] private Text txtStars;
  [SerializeField] private Text txtLetters;

  private int stageIndex = 0;
  private MenuController _parent;

  public StageMenuItem setStageIndex(int val)
  {
    stageIndex = val;
    txtStageIndex.text += (val + 1).ToString();
    return this;
  }

  public StageMenuItem setLabel(string val)
  {
    txtStageLabel.text = val.ToString();
    return this;
  }

  public StageMenuItem setStars(int val)
  {
    txtStars.text = val.ToString();
    return this;
  }

  public StageMenuItem setLetters(int val)
  {
    txtLetters.text = val.ToString();
    return this;
  }

  public StageMenuItem setParent(MenuController val)
  {
    _parent = val;
    buttonStage.onClick.AddListener(buttonHandler);
    return this;
  }

  private void buttonHandler()
  {
    _parent.selectStageButtonHandler(stageIndex);
  }
}
