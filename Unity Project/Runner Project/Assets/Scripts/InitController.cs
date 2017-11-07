using UnityEngine;

public class InitController : MonoBehaviour
{
  private GameMaster master;

  void Start()
  {
    master = GameMaster.getInstance();

    // TODO: Load local file configuration, saved score or whatever...
    /*
     * First scene to load GameMaster object. Load local configuration files and such... After we are ready we go to Menus Scene.
     */

    // Temporal delayer...
    Invoke("goToMenus", 0.1f);
  }

  private void goToMenus()
  {
    master.goToScene(1);
  }
}
