using UnityEngine;

public class AudioUserInterface : AudioObject
{
  [SerializeField]
  private AudioClip _accept;
  [SerializeField]
  private AudioClip _cancel;
  [SerializeField]
  private AudioClip _toggle;
  [SerializeField]
  private AudioClip _eraseData;
  [SerializeField]
  private AudioClip _pickUpOrb;
  [SerializeField]
  private AudioClip _pickUpLetter;
  [SerializeField]
  private AudioClip _newHighScore;
  [SerializeField]
  private AudioClip _allLettersCollected;

  public void Accept()
  {
    _source.PlayOneShot(_accept);
  }

  public void Cancel()
  {
    _source.PlayOneShot(_cancel);
  }

  public void Toggle()
  {
    _source.PlayOneShot(_toggle);
  }

  public void EraseData()
  {
    _source.PlayOneShot(_eraseData);
  }

  public void PickUpOrb()
  {
    _source.PlayOneShot(_pickUpOrb);
  }

  public void PickUpLetter()
  {
    _source.PlayOneShot(_pickUpLetter);
  }

  public void NewHighScore()
  {
    _source.PlayOneShot(_newHighScore);
  }

  public void AllLettersCollected()
  {
    _source.PlayOneShot(_allLettersCollected);
  }
}
