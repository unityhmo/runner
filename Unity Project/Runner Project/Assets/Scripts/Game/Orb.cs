using UnityEngine;

public class Orb : MonoBehaviour
{
  void Awake()
  {
    GameObject gameController = GameObject.FindGameObjectWithTag(BaseValues.TAG_GAME_CONTROLLER);

    if (gameController)
    {
      gameController.GetComponent<GameController>().OrbDetected();
    }
  }

  public void CollisionDetected()
  {
    Destroy(gameObject);
  }
}
