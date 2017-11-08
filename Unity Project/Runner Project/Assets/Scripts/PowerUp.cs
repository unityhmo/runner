using UnityEngine;

// Base class for any pickup that the player can grab
// Destruction behavoir and animations will be triggered here.
public class PowerUp : MonoBehaviour
{
  public void collisionDetected()
  {
    Debug.Log(gameObject.name + " collisioned!");
    Destroy(gameObject);
  }
}
