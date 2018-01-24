using UnityEngine;

// Component in charge of only reading user's input and sending them to PlayerController
public class PlayerInput : MonoBehaviour
{
  [SerializeField]
  private float _screenSwipePercent = 20; // percentage of screen swiped
  private float _swipeDistance;
  private float _tapDistance;
  private bool _swipeActionDetected;
  private Vector2 _fp; // first finger position
  private Vector2 _lp; // last finger position
  private PlayerController _contr;

  void Start()
  {
    _contr = transform.GetComponent<PlayerController>();

    _screenSwipePercent = Mathf.Clamp(_screenSwipePercent, 1, 99);
    _swipeDistance = Screen.width * (_screenSwipePercent / 100);
    _tapDistance = _swipeDistance / 3f;
  }

  void LateUpdate()
  {
    if (Input.touches.Length > 0)
    {
      if (!_contr.IsPlayable && !_contr.IsGameOver)
      {
        _contr.StartGame();
        return;
      }
      
      Touch touch = Input.touches[0];
      if (touch.phase == TouchPhase.Began)
      {
        _fp = touch.position;
        _lp = touch.position;
        _swipeActionDetected = false;
      }
      else if (touch.phase == TouchPhase.Moved)
      {
        _lp = touch.position;
        if (Mathf.Abs(_lp.x - _fp.x) >= _swipeDistance || Mathf.Abs(_lp.y - _fp.y) >= _swipeDistance)
        {
          _lp = _lp - _fp;
          if (Mathf.Abs(_lp.x) > Mathf.Abs(_lp.y))
          {
            if (_lp.x < 0f)
            {
              JumpLeft();
            }
            else if (_lp.x > 0f)
            {
              JumpRight();
            }
          }
          else
          {
            JumpUp();
          }

          _fp = touch.position;
          _lp = touch.position;
          _swipeActionDetected = true;
        }
      }
      else if (!_swipeActionDetected && touch.phase == TouchPhase.Ended && (Mathf.Abs(_lp.x - _fp.x) <= _tapDistance || Mathf.Abs(_lp.y - _fp.y) <= _tapDistance))
      {
        JumpUp();
      }
    }

    if (Input.anyKeyDown && !_contr.IsPlayable && !_contr.IsGameOver)
    {
      _contr.StartGame();
      return;
    }
    
    if (Input.GetKeyDown(KeyCode.LeftArrow))
      JumpLeft();
    else if (Input.GetKeyDown(KeyCode.RightArrow))
      JumpRight();
    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
      JumpUp();
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
