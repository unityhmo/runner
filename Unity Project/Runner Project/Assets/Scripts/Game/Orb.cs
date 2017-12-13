using UnityEngine;

public class Orb : MonoBehaviour
{
  [SerializeField]
  private GameObject _mesh;
  private Renderer _rend;
  [SerializeField]
  private float _speed = 250f;

  void Awake()
  {
    GameObject gameController = GameObject.FindGameObjectWithTag(BaseValues.TAG_GAME_CONTROLLER);

    if (gameController)
      gameController.GetComponent<GameController>().OrbDetected();

    if (_rend)
      _rend = _mesh.GetComponent<Renderer>();
  }

  void LateUpdate()
  {
    if (_rend && _rend.isVisible)
      transform.Rotate(Vector3.up, _speed * Time.deltaTime);
  }

  public void CollisionDetected()
  {
    Destroy(gameObject);
  }
}
