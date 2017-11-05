using UnityEngine;

public class MenusController : MonoBehaviour
{
  private GameMaster master;

  // Use this for initialization
  void Start()
  {
    master = GameMaster.getInstance();
  }

  public void loadScene(string name)
  {
    master.goToScene(name);
  }

  public void exitApp()
  {
    Debug.Log("Quit app");
    Application.Quit();
  }
}
