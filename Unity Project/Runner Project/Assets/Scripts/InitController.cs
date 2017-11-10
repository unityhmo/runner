using UnityEngine;

/*
 * Init Scene MAIN (and probably only) GameObject component.
 * Here we make sure we are ready displaying the correct resolution, user's options such sound toggle on/off.
 * This component does NOT load external data; but ensures GameMaster is ready and there were no errors.
 */
public class InitController : MonoBehaviour
{
  private GameMaster _master;

  void Start()
  {
    _master = GameMaster.GetInstance ();

    /* 
     * TODO: GameMaster has to load and apply values from outside. Then we check everything is alright and only then we move forward.
     */

    // Temporal delayer...
    Invoke ("GoToMenus", 0.1f);
  }

  private void GoToMenus ()
  {
    _master.GoToScene (1);
  }
}
