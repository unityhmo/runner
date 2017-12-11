using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  private Transform _player;
  [SerializeField]
  private float _speed = 10f;
  private float _distance;
  private Vector3 _endPos;

  [SerializeField]
  private float _victorySpeed = 1f;
  [SerializeField]
  private float _victoryZoomDistance = 3.85f;
  private bool _zoomOn = false;

  void Start()
  {
    _player = GameObject.FindGameObjectWithTag(BaseValues.TAG_PLAYER).transform;
    _distance = _player.position.z - transform.position.z;
  }

  void LateUpdate()
  {
    if (!_zoomOn)
    {
      transform.position = new Vector3(transform.position.x, transform.position.y, _player.position.z - _distance);

      _endPos = new Vector3(_player.position.x, transform.position.y, transform.position.z);
    }
    else
    {
      _endPos = new Vector3(_player.position.x, _player.position.y + _victoryZoomDistance, _player.position.z - _victoryZoomDistance);
    }

    transform.position = Vector3.Lerp(transform.position, _endPos, Time.deltaTime * _speed);
  }

  public void VictoryZoom()
  {
    _speed = _victorySpeed;
    _zoomOn = true;
  }
}
