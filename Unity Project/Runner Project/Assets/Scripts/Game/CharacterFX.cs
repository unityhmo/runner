using UnityEngine;

// In charge of shader FX, particles and other gameobjects for visual purposes.
public class CharacterFX : MonoBehaviour
{
  [SerializeField]
  private bool _hasLowLife = false;
  private Color _baseColor;
  private Renderer _rend;
  [SerializeField]
  private Color _lowHealthColor = Color.red;
  [SerializeField]
  private GameObject _mesh;
  private Shader _shaderStandard;
  private Shader _shaderUnlit;
  private float _shaderTimer = 0f;
  [SerializeField]
  private float _shaderTicSpeed = 1f;
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
    {
      AudioManager.GetCharFX().PlayLowHealth();
      _shaderOn = true;
    }
    else
    {
      AudioManager.GetCharFX().StopLowHealth();
      _shaderOn = false;
    }

    SetShaderState();
  }

  public void Death()
  {
    Debug.Log("Add: Death fx");
    AudioManager.GetCharFX().Death();
  }

  public void Jump()
  {
    Debug.Log("Add: Jump fx");
  }

  public void Landing()
  {
    Debug.Log("Add: Landing fx");
    AudioManager.GetCharFX().Land();
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
