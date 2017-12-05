using UnityEngine;

// Destruction behavoir and animations will be triggered here.
public class Obstacle : MonoBehaviour
{
  [SerializeField]
  private int _damage = 1;
  //private AudioSource _source;

  void Start()
  {
    //_source = GetComponent<AudioSource>();
  }

  public void CollisionDetected()
  {
    Debug.Log(gameObject.name + " collisioned!");
    Destroy(gameObject);
  }

  public int GetDamage()
  {
    return _damage;
  }
}
