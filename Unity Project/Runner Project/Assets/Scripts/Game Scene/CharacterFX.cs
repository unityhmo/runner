using UnityEngine;

public class CharacterFX : MonoBehaviour
{
  [SerializeField]
  private bool hasLowLife = false;

  void LateUpdate()
  {
    if (hasLowLife)
    {
      // TODO: Animation of low life
    }
  }

  public void setLowLife(bool val = false)
  {
    hasLowLife = val;
  }

  public void death()
  {
    Debug.Log("Add: Death fx");
  }

  public void jump()
  {
    Debug.Log("Add: Jump fx");
  }

  public void damage()
  {
    Debug.Log("Add: Damage fx");
  }
}
