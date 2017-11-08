using UnityEngine;

// In charge of shader FX, particles and other gameobjects for visual purposes.
public class CharacterFX : MonoBehaviour
{
  [SerializeField] private bool hasLowLife = false;
  private Color baseColor; // Saves Standard original color
  private Renderer rend;
  [SerializeField] private Color lowHealthColor = Color.red; // Low health color, Red is good, huh?
  [SerializeField] private GameObject mesh;
  private Shader shaderStandard;
  private Shader shaderUnlit;

  // Shader swapper timer variables...
  private float shaderTimer = 0f;
  [SerializeField] private float shaderTicSpeed = 1f;
  private bool shaderOn = false;

  void Start()
  {
    rend = mesh.GetComponent<Renderer>();
    baseColor = rend.material.GetColor("_Color");

    shaderStandard = Shader.Find("Standard");
    // NOTE: Unlit Shader doesnt support shadows.
    shaderUnlit = Shader.Find("Unlit/Color");
  }

  void LateUpdate()
  {
    // If low health, keep tic timer running/looping
    if (hasLowLife)
    {
      shaderTimer += Time.deltaTime;
      if (shaderTimer > shaderTicSpeed)
      {
        shaderTimer = 0f;
        shaderOn = !shaderOn;
        setShaderState();
      }
    }
  }

  public void setLowLife(bool val)
  {
    hasLowLife = val;
    shaderTimer = 0f;

    if (hasLowLife)
      shaderOn = true;
    else
      shaderOn = false;

    setShaderState();
  }

  public void death()
  {
    Debug.Log("Add: Death fx");
  }

  public void jump()
  {
    Debug.Log("Add: Jump fx");
  }

  public void damage()
  {
    Debug.Log("Add: Damage fx");
  }

  // Swapper of Shader type and Color for each shader.
  private void setShaderState()
  {
    if (shaderOn)
    {
      rend.material.shader = shaderUnlit;
      rend.material.SetColor("_Color", lowHealthColor);
    }
    else
    {
      rend.material.shader = shaderStandard;
      rend.material.SetColor("_Color", baseColor);
    }
  }
}
