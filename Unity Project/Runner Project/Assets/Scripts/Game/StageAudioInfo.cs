using UnityEngine;

public class StageAudioInfo : MonoBehaviour
{
  [SerializeField]
  private AudioClip _intro;
  [SerializeField]
  private AudioClip _loopMain;

  void Start()
  {
    if (_loopMain != null)
      AudioManager.GetMFX().SetClip(_loopMain, _intro);
  }
}
