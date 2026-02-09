using UnityEngine;

public class StageAsset : MonoBehaviour
{
    [SerializeField] private AudioClip _intro;
    [SerializeField] private AudioClip _loopMain;
    [SerializeField] private Color _backgroundColor;

    private void Start()
    {
        AudioMusic stageMusic = AudioManager.GetMFX();
        if (_loopMain != null && stageMusic)
            stageMusic.SetClip(_loopMain, _intro);

        Camera.main.backgroundColor = _backgroundColor;
    }
}
