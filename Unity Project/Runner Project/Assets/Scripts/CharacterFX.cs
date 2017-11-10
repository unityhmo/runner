using UnityEngine;

// In charge of shader FX, particles and other gameobjects for visual purposes.
public class CharacterFX : MonoBehaviour
{
  [SerializeField] private bool _hasLowLife = false;
  private Color _baseColor; // Saves Standard original color
  private Renderer _rend;
  [SerializeField] private Color _lowHealthColor = Color.red; // Low health color, Red is good, huh?
  [SerializeField] private GameObject _mesh;
  private Shader _shaderStandard;
  private Shader _shaderUnlit;

  // Shader swapper timer variables...
  private float _shaderTimer = 0f;
  [SerializeField] private float _shaderTicSpeed = 1f;
  private bool _shaderOn = false;

  void Start()
  {
    _rend = _mesh.GetComponent<Renderer>();
    _baseColor = _rend.material.GetColor("_Color");

    _shaderStandard = Shader.Find("Standard");
    // NOTE: Unlit Shader doesnt support shadows.
    _shaderUnlit = Shader.Find("Unlit/Color");
  }

  void LateUpdate()
  {
    // If low health, keep tic timer running/looping
    if (_hasLowLife)
    {
      _shaderTimer += Time.deltaTime;
      if (_shaderTimer > _shaderTicSpeed)
      {
        _shaderTimer = 0f;
        _shaderOn = !_shaderOn;
        SetShaderState();
      }
    }
  }

  public void SetLowLife(bool val)
  {
    _hasLowLife = val;
    _shaderTimer = 0f;

    if (_hasLowLife)
      _shaderOn = true;
    else
      _shaderOn = false;

    SetShaderState();
  }

  public void Death()
  {
    Debug.Log("Add: Death fx");
  }

  public void Jump()
  {
    Debug.Log("Add: Jump fx");
  }

  public void Damage()
  {
    Debug.Log("Add: Damage fx");
  }

  // Swapper of Shader type and Color for each shader.
  private void SetShaderState()
  {
    if (_shaderOn)
    {
      _rend.material.shader = _shaderUnlit;
      _rend.material.SetColor("_Color", _lowHealthColor);
    }
    else
    {
      _rend.material.shader = _shaderStandard;
      _rend.material.SetColor("_Color", _baseColor);
    }
  }
}
