using UnityEngine;

// Component in charge of managing the Animator state machine
public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    private Animator _anim;
    private bool _isRunning;
    private bool _isGrounded;

    void Awake()
    {
        _anim = transform.GetComponent<Animator>();
    }

    // Player input actions
    public void JumpLeft()
    {
        if (_isRunning)
            _anim.Play("left");
    }
    public void JumpRight()
    {
        if (_isRunning)
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

    private void Update()
    {
        if (_isGrounded == _playerController.IsGrounded) return;
        _isGrounded = _playerController.IsGrounded;
        _anim.SetBool("isGrounded", _isGrounded);
    }
}
