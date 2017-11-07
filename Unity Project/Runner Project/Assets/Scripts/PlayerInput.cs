using UnityEngine;

// Component in charge of only reading user's input and sending them to PlayerController
public class PlayerInput : MonoBehaviour
{
  [SerializeField]
  private float swipeDistance = 50;
  // first finger position
  private Vector2 fp;
  // last finger position
  private Vector2 lp;

  private PlayerController contr;

  void Start()
  {
    contr = transform.GetComponent<PlayerController>();
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
        fp = touch.position;
        lp = touch.position;
      }

      if (touch.phase == TouchPhase.Moved)
      {
        lp = touch.position;
      }

      if (touch.phase == TouchPhase.Ended)
      {
        if ((fp.x - lp.x) > swipeDistance) // left
        {
          jumpLeft();
        }
        else if ((fp.x - lp.x) < -swipeDistance) // right
        {
          jumpRight();
        }
        else if ((fp.y - lp.y) < -swipeDistance) // up
        {
          jumpUp();
        }
      }
    }

    if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
      jumpLeft();
    }
    else if (Input.GetKeyDown(KeyCode.RightArrow))
    {
      jumpRight();
    }
    else if (Input.GetKeyDown(KeyCode.UpArrow))
    {
      jumpUp();
    }
  }

  private void jumpLeft()
  {
    contr.jumpLeft();
  }

  private void jumpRight()
  {
    contr.jumpRight();
  }

  private void jumpUp()
  {
    contr.jumpUp();
  }
}
