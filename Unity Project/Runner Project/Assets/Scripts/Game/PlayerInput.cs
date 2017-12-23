using UnityEngine;

// Component in charge of only reading user's input and sending them to PlayerController
public class PlayerInput : MonoBehaviour
{
  [SerializeField]
  private float _screenSwipePercent = 20; // percentage of screen swiped
  private float _swipeDistance;
  private Vector2 _fp; // first finger position
  private Vector2 _lp; // last finger position
  private PlayerController _contr;

  void Start()
  {
    _contr = transform.GetComponent<PlayerController>();

    _screenSwipePercent = Mathf.Clamp(_screenSwipePercent, 1, 99);
    _swipeDistance = Screen.width / _screenSwipePercent;
  }

  /*
   * Let's face it, we humans are slow (compared to computers), there is no need to use computing resources in reading X times per frame our inputs. We send our input management to LateUpdate.
   */
  void LateUpdate()
  {
    if (Input.touches.Length > 0)
    {
      Touch touch = Input.touches[0];
      if (touch.phase == TouchPhase.Began)
      {
        _fp = touch.position;
        _lp = touch.position;
      }
      else if (touch.phase == TouchPhase.Moved)
      {
        _lp = touch.position;
        if (Vector2.Distance(_lp, _fp) >= _swipeDistance)
        {
          _lp = _lp - _fp;
          if (Mathf.Abs(_lp.x) > Mathf.Abs(_lp.y))
          {
            if (_lp.x < 0f)
              JumpLeft();
            else if (_lp.x > 0f)
              JumpRight();
          }
          else
            JumpUp();
        }
      }
    }

    if (Input.GetKeyDown(KeyCode.LeftArrow))
      JumpLeft();
    else if (Input.GetKeyDown(KeyCode.RightArrow))
      JumpRight();
    else if (Input.GetKeyDown(KeyCode.UpArrow))
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
