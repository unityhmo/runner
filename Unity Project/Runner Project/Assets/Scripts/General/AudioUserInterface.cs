using UnityEngine;

public class AudioUserInterface : AudioObject
{
  [SerializeField]
  private AudioClip _accept;
  [SerializeField]
  private AudioClip _cancel;
  [SerializeField]
  private AudioClip _pickUpOrb;
  [SerializeField]
  private AudioClip _newHighScore;

  public void Accept()
  {
    _source.PlayOneShot(_accept);
  }

  public void Cancel()
  {
    _source.PlayOneShot(_cancel);
  }

  public void PickUpOrb()
  {
    _source.PlayOneShot(_pickUpOrb);
  }

  public void NewHighScore()
  {
    _source.PlayOneShot(_newHighScore);
  }
}
