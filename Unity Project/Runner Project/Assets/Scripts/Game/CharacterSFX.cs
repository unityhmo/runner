using UnityEngine;

public class CharacterSFX : AudioObject
{
  [SerializeField]
  private AudioClip _dash;
  [SerializeField]
  private AudioClip _jump;
  [SerializeField]
  private AudioClip _land;
  [SerializeField]
  private AudioClip _damage;
  [SerializeField]
  private AudioClip _death;

  void Start()
  {
    AudioManager.SetCharFX(this);
  }

  public void Dash()
  {
    _source.PlayOneShot(_dash);
  }

  public void Jump()
  {
    _source.PlayOneShot(_jump);
  }

  public void Land()
  {
    _source.PlayOneShot(_land);
  }

  public void Damage()
  {
    _source.PlayOneShot(_damage);
  }

  public void Death()
  {
    _source.PlayOneShot(_death);
  }

  public void PlayLowHealth()
  {
    _source.Play();
  }

  public void StopLowHealth()
  {
    _source.Stop();
  }
}