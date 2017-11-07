using UnityEngine;

public class Enemy : MonoBehaviour
{
  public void collisionDetected()
  {
    Debug.Log(gameObject.name + " collisioned!");
    Destroy(gameObject);
  }
}
