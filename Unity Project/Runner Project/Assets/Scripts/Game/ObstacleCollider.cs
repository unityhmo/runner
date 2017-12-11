using UnityEngine;

public class ObstacleCollider : MonoBehaviour
{
  private Obstacle _parent;

  public void SetParent(Obstacle parent)
  {
    _parent = parent;
  }

  public void CollisionDetected()
  {
    _parent.CollisionDetected();
  }

  public int Damage
  {
    get
    {
      return _parent.Damage;
    }
  }
}
