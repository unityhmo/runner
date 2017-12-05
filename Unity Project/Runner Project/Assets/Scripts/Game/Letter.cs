using UnityEngine;
// Destruction behavoir and animations will be triggered here.
public class Letter : MonoBehaviour
{
  public void CollisionDetected()
  {
    Debug.Log(gameObject.name + " collisioned!");
    Destroy(gameObject);
  }
}
