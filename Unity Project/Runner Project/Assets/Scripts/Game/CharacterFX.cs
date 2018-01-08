using UnityEngine;
using UnityEngine.UI;

// In charge of shader FX, particles and other gameobjects for visual purposes.
public class CharacterFX : MonoBehaviour
{
  [SerializeField]
  private bool _hasLowLife = false;
  private Material _baseMaterial;
  private Renderer _rend;
  [SerializeField]
  private Material _hurtMaterial;
  [SerializeField]
  private GameObject _mesh;
  private float _shaderTimer = 0f;
  [SerializeField]
  private float _shaderTicSpeed = 1f;
  private bool _shaderOn = false;
  private bool _hasDamage = false;
  private float _damageTimer;
  private float _invincibilityTimer;
  [SerializeField]
  private RawImage _damageOverlay;
  private Color _overlayColor;
  private float _t;
  [SerializeField]
  private GameObject _deathFX;
  [SerializeField]
  private GameObject _hitsFX;

  void Start()
  {
    _rend = _mesh.GetComponent<Renderer>();
    _baseMaterial = _rend.material;

    _overlayColor = _damageOverlay.color;
  }

  void LateUpdate()
  {
    // If low health, keep tic timer running/looping
    if (_hasLowLife || _hasDamage)
    {
      _shaderTimer += Time.deltaTime;
      if (_shaderTimer > _shaderTicSpeed)
      {
        _shaderTimer = 0f;
        _shaderOn = !_shaderOn;
        SetShaderState();
      }

      if (_hasDamage)
      {
        _damageTimer += Time.deltaTime;

        _overlayColor.a = Mathf.Lerp(1f, 0f, _t);
        _t += Time.deltaTime / _invincibilityTimer;

        if (_damageTimer > _invincibilityTimer)
        {
          _shaderTimer = 0f;
          _shaderOn = false;
          SetShaderState();
          _damageOverlay.enabled = false;
          _hasDamage = false;
        }

        _damageOverlay.color = _overlayColor;
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
    CreateParticle(_deathFX);
    AudioManager.GetCharFX().Death();
    Invoke("DestroyMe", 1f);
  }

  public void Jump()
  {
    // Dash and Jump particles here
  }

  public void Landing()
  {
    Debug.Log("Add: Landing fx");
    AudioManager.GetCharFX().Land();
  }

  public void Damage()
  {
    _t = 0f;
    _damageTimer = 0f;
    _damageOverlay.enabled = true;
    _hasDamage = true;
  }

  public void CreateHitsFX()
  {
    CreateParticle(_hitsFX);
  }

  private void CreateParticle(GameObject go)
  {
    GameObject particle = Instantiate(go, transform);
    Vector3 pos = transform.position;
    pos.y += 1f;
    pos.z += -0.2f;
    particle.transform.position = pos;
  }

  public void SetInvincibilityTimer(float invincibilityTimer)
  {
    _invincibilityTimer = invincibilityTimer;
  }

  // Swapper of Shader type and Color for each shader.
  private void SetShaderState()
  {
    if (_shaderOn)
      _rend.material = _hurtMaterial;
    else
      _rend.material = _baseMaterial;
  }

  private void DestroyMe()
  {
    Destroy(_mesh);
  }
}
