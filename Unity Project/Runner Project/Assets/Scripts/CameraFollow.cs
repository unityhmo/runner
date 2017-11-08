using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  private Transform player;
  [SerializeField] private float speed = 10f;
  static float distance;

  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    distance = player.position.z - transform.position.z;
  }

  void LateUpdate()
  {
    transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z - distance);

    transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
  }
}
