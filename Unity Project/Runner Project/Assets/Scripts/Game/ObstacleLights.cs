using UnityEngine;

public class ObstacleLights : MonoBehaviour
{
  [SerializeField]
  private bool _lightsOn = true;
  private bool _currentLightsOn;
  [SerializeField]
  private Material _on;
  [SerializeField]
  private Material _off;
  [SerializeField]
  private float _tic;
  private float _currentTime;
  [SerializeField]
  private GameObject _rendererHolder;
  private Renderer _renderer;

  void Start()
  {
    if (_rendererHolder == null)
      return;

    _renderer = _rendererHolder.GetComponent<Renderer>();

    if (!_lightsOn && _off != null)
      TurnOff();
    else if (_lightsOn && _on != null)
      TurnOn();
  }

  void LateUpdate()
  {
    if (_renderer != null)
    {
      if (_lightsOn && _tic > 0 && _off != null && _on != null)
      {
        if (_currentTime >= _tic)
        {
          if (_currentLightsOn)
            TurnOff();
          else
            TurnOn();

          _currentTime = 0f;
        }
        else
          _currentTime += Time.deltaTime;
      }
    }
  }

  private void TurnOff()
  {
    _renderer.material = _off;
    _currentLightsOn = false;
  }

  private void TurnOn()
  {
    _renderer.material = _on;
    _currentLightsOn = true;
  }
}
