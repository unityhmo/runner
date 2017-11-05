using UnityEngine;

public class PowerUp : MonoBehaviour
{
  public void collisionDetected()
  {
    Debug.Log(gameObject.name + " collisioned!");
    Destroy(gameObject);
  }
}
