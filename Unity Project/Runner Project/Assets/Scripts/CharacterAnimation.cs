using UnityEngine;

// Component in charge of managing the Animator state machine
public class CharacterAnimation : MonoBehaviour
{
  private Animator _anim;
  private CharacterController _contr;
  private bool _isRunning = false;
  [SerializeField] private bool _isGrounded = false;

  void Awake()
  {
    _anim = transform.GetComponent<Animator>();
    _contr = transform.GetComponent<CharacterController>();
  }

  // Player input actions
  public void JumpLeft()
  {
    if (_isGrounded && _isRunning)
      _anim.Play("left");
  }
  public void JumpRight()
  {
    if (_isGrounded && _isRunning)
      _anim.Play("right");
  }
  public void JumpUp()
  {
    if (_isGrounded && _isRunning)
      _anim.Play("jump");
  }

  // Level triggered actions
  public void ToggleRunning()
  {
    _isRunning = !_anim.GetBool("isRunning");
    SetBoolRunning();
  }
  public void SetRunning(bool state)
  {
    _isRunning = state;
    SetBoolRunning();
  }
  public void Victory()
  {
    SetRunning(false);
    _anim.Play("victory");
  }
  public void Damage()
  {
    if (_isRunning)
      _anim.Play("damage");
  }
  public void Death()
  {
    _anim.Play("death");
  }

  private void SetBoolRunning()
  {
    _anim.SetBool("isRunning", _isRunning);
  }

  /*
   * Since animation and visuals aren't critical for execution, we keep all our frame by frame code in LateUpdate
   */
  void LateUpdate()
  {
    _isGrounded = _contr.isGrounded;
    _anim.SetBool("isGrounded", _isGrounded);
  }
}
