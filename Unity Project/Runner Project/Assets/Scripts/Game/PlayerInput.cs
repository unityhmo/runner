using UnityEngine;

// Component in charge of only reading user's input and sending them to PlayerController
public class PlayerInput : MonoBehaviour
{
  [SerializeField]
  private float _swipeDistance = 50;
  // first finger position
  private Vector2 _fp;
  // last finger position
  private Vector2 _lp;

  private PlayerController _contr;

  void Start()
  {
    _contr = transform.GetComponent<PlayerController>();
  }

  /*
   * Let's face it, we humans are slow (compared to computers), there is no need to use computing resources in reading X times per frame our inputs. We send our input management to LateUpdate.
   */
  void LateUpdate()
  {
    foreach (Touch touch in Input.touches)
    {
      if (touch.phase == TouchPhase.Began)
      {
        _fp = touch.position;
        _lp = touch.position;
      }

      if (touch.phase == TouchPhase.Moved)
      {
        _lp = touch.position;
      }

      if (touch.phase == TouchPhase.Ended)
      {
        _lp = _lp - _fp;
        if ((_lp.x) < -_swipeDistance && -_lp.x > _lp.y) // left
        {
          JumpLeft();
        }
        else if ((_lp.x) > _swipeDistance && _lp.x > _lp.y) // right
        {
          JumpRight();
        }
        else if ((_lp.y) > _swipeDistance) // up
        {
          JumpUp();
        }
      }
    }

    if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
      JumpLeft();
    }
    else if (Input.GetKeyDown(KeyCode.RightArrow))
    {
      JumpRight();
    }
    else if (Input.GetKeyDown(KeyCode.UpArrow))
    {
      JumpUp();
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
