using UnityEngine;

public class ParticleSystemDestroyer : MonoBehaviour
{
  private ParticleSystem ps;

  public void Awake()
  {
    ps = GetComponent<ParticleSystem>();
  }

  public void LateUpdate()
  {
    if (ps && !ps.IsAlive())
    {
      Destroy(gameObject);
    }
  }
}