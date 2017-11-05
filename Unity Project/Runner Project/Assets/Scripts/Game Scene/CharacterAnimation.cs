using UnityEngine;

// Component in charge of managing the Animator state machine
public class CharacterAnimation : MonoBehaviour
{
  private Animator anim;
  private CharacterController contr;
  private bool isRunning = false;
  [SerializeField]
  private bool isGrounded = false;

  void Awake()
  {
    anim = transform.GetComponent<Animator>();
    contr = transform.GetComponent<CharacterController>();
  }

  // Player input actions
  public void jumpLeft()
  {
    if (isGrounded && isRunning)
      anim.Play("left");
  }
  public void jumpRight()
  {
    if (isGrounded && isRunning)
      anim.Play("right");
  }
  public void jumpUp()
  {
    if (isGrounded && isRunning)
      anim.Play("jump");
  }

  // Level triggered actions
  public void toggleRunning()
  {
    isRunning = !anim.GetBool("isRunning");
    setBoolRunning();
  }
  public void setRunning(bool state)
  {
    isRunning = state;
    setBoolRunning();
  }
  public void victory()
  {
    setRunning(false);
    anim.Play("victory");
  }
  public void damage()
  {
    if (isRunning)
      anim.Play("damage");
  }
  public void death()
  {
    anim.Play("death");
  }

  private void setBoolRunning()
  {
    anim.SetBool("isRunning", isRunning);
  }

  /*
   * Since animation and visuals aren't critical for execution, we keep all our frame by frame code in LateUpdate
   */
  void LateUpdate()
  {
    isGrounded = contr.isGrounded;
    anim.SetBool("isGrounded", isGrounded);
  }
}
