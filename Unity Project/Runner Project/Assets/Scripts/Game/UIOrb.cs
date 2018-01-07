using UnityEngine;

public class UIOrb : MonoBehaviour
{
  [SerializeField]
  private GameObject _mesh;
  private Renderer _rend;
  [SerializeField]
  private float _speed = 250f;

  void Awake()
  {
    if (_mesh)
      _rend = _mesh.GetComponent<Renderer>();
  }

  void LateUpdate()
  {
    if (_rend && _rend.isVisible)
      transform.Rotate(Vector3.up, _speed * Time.deltaTime);
  }
}
