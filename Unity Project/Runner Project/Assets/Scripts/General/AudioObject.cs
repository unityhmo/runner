using UnityEngine;

public class AudioObject : MonoBehaviour
{
  
  protected AudioSource _source;

  protected void Awake()
  {
    _source = GetComponent<AudioSource>();
  }

  public void SetSound(bool value)
  {
    _source.enabled = value;
  }
}
