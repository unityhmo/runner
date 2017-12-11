using UnityEngine;
using System.Collections;

public class AudioMusic : AudioObject
{
  private AudioClip _themeIntro;
  private AudioClip _themeLoop;
  private IEnumerator _coroutine;

  public void SetClip(AudioClip loop, AudioClip intro = null)
  {
    if (_coroutine != null)
    {
      StopCoroutine(_coroutine);
      _coroutine = null;
    }
    _source.Stop();
    _source.loop = false;

    _themeIntro = intro;
    _themeLoop = loop;

    if (_themeIntro != null)
    {
      _coroutine = PlayIntroLoopTrack();
      StartCoroutine(_coroutine);
    }
    else
      PlayLoop();
  }

  IEnumerator PlayIntroLoopTrack()
  {
    _source.clip = _themeIntro;
    _source.Play();

    yield return new WaitForSeconds(_source.clip.length);

    PlayLoop();
  }

  private void PlayLoop()
  {
    _source.clip = _themeLoop;
    _source.loop = true;
    _source.Play();
  }
}
