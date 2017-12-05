using UnityEngine;

public class StageAudioInfo : MonoBehaviour
{
  [SerializeField]
  private AudioClip _intro;
  [SerializeField]
  private AudioClip _loopMain;

  void Start()
  {
    AudioMusic stageMusic = AudioManager.GetMFX();
    if (_loopMain != null && stageMusic)
      stageMusic.SetClip(_loopMain, _intro);
  }
}
