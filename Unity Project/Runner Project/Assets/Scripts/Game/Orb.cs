using UnityEngine;

public class Orb : MonoBehaviour
{
  public void CollisionDetected()
  {
    Destroy(gameObject);
  }
}
