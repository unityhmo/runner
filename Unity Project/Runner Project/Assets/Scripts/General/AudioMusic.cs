using UnityEngine;
using System.Collections;

public class AudioMusic : AudioObject
{
  private AudioClip _themeIntro;
  private AudioClip _themeLoop;
  private bool _hasTwoClips = false;

  public void SetClip(AudioClip loop, AudioClip intro = null)
  {
    AudioClip clip;

    _themeLoop = loop;
    clip = _themeLoop;

    if (intro != null)
    {
      _hasTwoClips = true;
      _themeIntro = intro;
      clip = _themeIntro;
    }

    PlayClip(clip);
  }

  private void PlayClip(AudioClip clip)
  {
    _source.Stop();
    _source.clip = clip;
    _source.Play();

    if (_hasTwoClips)
      StartCoroutine(PlayIntroLoopTrack());
  }

  IEnumerator PlayIntroLoopTrack()
  {
    yield return new WaitForSeconds(_source.clip.length);
    _source.clip = _themeLoop;
    _source.Play();
  }
}
