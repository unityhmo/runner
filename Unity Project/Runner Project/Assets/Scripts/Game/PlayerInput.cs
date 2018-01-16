using UnityEngine;

// Component in charge of only reading user's input and sending them to PlayerController
public class PlayerInput : MonoBehaviour
{
  [SerializeField]
  private float _screenSwipePercent = 20; // percentage of screen swiped
  private float _swipeDistance;
  private int _touchBeganCounter = 0;
  private bool _swipeActionDetected = false;
  private Vector2 _fp; // first finger position
  private Vector2 _lp; // last finger position
  private PlayerController _contr;

  void Start()
  {
    _contr = transform.GetComponent<PlayerController>();

    _screenSwipePercent = Mathf.Clamp(_screenSwipePercent, 0.01f, 100);
    _swipeDistance = Screen.width * (_screenSwipePercent / 100);
  }

  void LateUpdate()
  {
    if (Input.GetKeyDown(KeyCode.LeftArrow))
      JumpLeft();
    else if (Input.GetKeyDown(KeyCode.RightArrow))
      JumpRight();
    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
      JumpUp();
  }

  void FixedUpdate()
  {
    if (Input.touches.Length > 0)
    {
      Touch touch = Input.touches[0];
      if (touch.phase == TouchPhase.Began)
      {
        _fp = touch.position;
        _lp = touch.position;
        _swipeActionDetected = false;
        _touchBeganCounter = 1;
      }
      else if (touch.phase == TouchPhase.Moved && !_swipeActionDetected && _touchBeganCounter == 1)
      {
        _lp = touch.position;
        if (_lp.y > _fp.y + _swipeDistance) {
          JumpUp ();
        } else {
          if (_lp.x < _fp.x) {
            JumpLeft ();
          } else {
            JumpRight ();
          }
        }
        _fp = touch.position;
        _swipeActionDetected = true;
      }
      else if (touch.phase == TouchPhase.Ended)
      {
        if (!_swipeActionDetected && _touchBeganCounter == 1) {
          
          _lp = touch.position;
          if (Mathf.Abs(_lp.x - _fp.x) <= _swipeDistance || Mathf.Abs(_lp.y - _fp.y) <= _swipeDistance)
          {
            JumpUp ();
          }
          _swipeActionDetected = false;
          _touchBeganCounter--;
        }
      }
    }
  }

  private void JumpLeft()
  {
    _contr.JumpLeft();
  }

  private void JumpRight()
  {
    _contr.JumpRight();
  }

  private void JumpUp()
  {
    _contr.JumpUp();
  }

}
