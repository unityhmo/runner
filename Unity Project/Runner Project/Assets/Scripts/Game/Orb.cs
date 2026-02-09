using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField]
    private GameObject _mesh;
    private Renderer _rend;
    [SerializeField]
    private float _speed = 250f;

    private void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag(BaseValues.TAG_GAME_CONTROLLER);

        if (gameController)
            gameController.GetComponent<GameController>().OrbDetected();

        if (_mesh)
            _rend = _mesh.GetComponent<Renderer>();

        transform.localEulerAngles = new Vector3(0f, Random.Range(-180f, 180f), 0f);
    }

    private void LateUpdate()
    {
        if (_rend && _rend.isVisible)
            transform.Rotate(Vector3.up, _speed * Time.deltaTime);
    }

    public void CollisionDetected()
    {
        Destroy(gameObject);
    }
}
