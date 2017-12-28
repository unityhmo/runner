using UnityEngine;

// Destruction behavoir and animations will be triggered here.
public class Obstacle : MonoBehaviour
{
  [SerializeField]
  private int _damage = 1;
  private AudioSource _source;
  [SerializeField]
  private AudioClip _destroyClip;
  [SerializeField]
  private GameObject[] _colliders;

  void Start()
  {
    _source = GetComponent<AudioSource>();

    if (_colliders.Length > 0)
    {
      for (int i = 0; i < _colliders.Length; i++)
      {
        _colliders[i].GetComponent<ObstacleCollider>().SetParent(this);
      }
    }
  }

  public void CollisionDetected()
  {
    if (_source != null && _destroyClip != null)
      _source.PlayOneShot(_destroyClip);

    //Destroy(gameObject);
  }

  public int Damage
  {
    get {
      return _damage;
    }
  }
}
