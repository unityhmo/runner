using UnityEngine;

// Base class for any obstacle that can damage the player
// Destruction behavoir and animations will be triggered here.
public class Enemy : MonoBehaviour
{
  public void CollisionDetected()
  {
    Debug.Log(gameObject.name + " collisioned!");
    Destroy(gameObject);
  }
}
