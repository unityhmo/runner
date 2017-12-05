using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  private Transform _player;
  [SerializeField] private float _speed = 10f;
  static float _distance;

  void Start()
  {
    _player = GameObject.FindGameObjectWithTag(BaseValues.TAG_PLAYER).transform;
    _distance = _player.position.z - transform.position.z;
  }

  void LateUpdate()
  {
    transform.position = new Vector3(transform.position.x, transform.position.y, _player.position.z - _distance);

    transform.position = Vector3.Lerp(transform.position, new Vector3(_player.position.x, transform.position.y, transform.position.z), Time.deltaTime * _speed);
  }
}
