using UnityEngine;

/*
 * Using OnGUI we draw a black rectangle that covers
 * all contents as courtines.
 * This class works along with GameMaster component.
 * Once fadeIn is done, it callsback master so it loads
 * destination scene. After loading the new scene master
 * tells this component to initiate fadeOut. Finally after the back rectangle is gone, we tell master we are done, and we destroy this gameobject. Adios!
 */
public class FadeInFadeOut : MonoBehaviour
{
  private GameMaster _master;
  private int destinationScene;
  private float fIntroDuration = 0.5f;
  private float fOutroDuration = 0.5f;
  private float fCurrentDuration;
  private float fAlpha;
  private bool blnTweenDone;
  private bool blnIntroDone;
  private bool blnOutroDone;
  private bool blnStartAnimation;
  private Color solidColor;
  private Texture2D solidRect;

  void Awake()
  {
    _master = GameMaster.getInstance();
    DontDestroyOnLoad(transform.gameObject);
    blnTweenDone = false;
    blnIntroDone = false;
    blnOutroDone = false;
    blnStartAnimation = false;
    solidColor = Color.black;
    solidRect = new Texture2D(1, 1);
    solidRect.SetPixel(0, 0, Color.red);
    solidRect.Apply();
  }

  // STEP 1: Fade in
  public void fadeIn(int sceneIndex)
  {
    destinationScene = sceneIndex;
    fCurrentDuration = 0f;
    blnStartAnimation = true;
  }

  // STEP 3: After master obj loads destination scene, we start Fading out
  public void fadeOut()
  {
    fCurrentDuration = 0f;
    blnStartAnimation = true;
    blnTweenDone = false;
    blnIntroDone = true;
  }

  // Draws the fading in&out black rectangle
  void OnGUI()
  {
    if (blnStartAnimation)
    {
      GUI.depth = -1;

      Rect rectIntroSize = new Rect(0, 0, Screen.width, Screen.height);

      solidColor.a = fAlpha;
      GUI.color = solidColor;
      GUI.DrawTexture(rectIntroSize, solidRect);
    }
  }

  void LateUpdate()
  {
    if (blnStartAnimation)
    {
      fCurrentDuration += Time.deltaTime;

      // Intro modificator
      if (!blnTweenDone && !blnIntroDone)
      {
        fAlpha = Mathf.Lerp(0, 1, fCurrentDuration / fIntroDuration);
        if (fAlpha == 1)
        {
          // STEP 2: Fade in callback to GameMaster obj
          blnTweenDone = true;
          blnIntroDone = true;
          // Sends callback to parent gobject telling fadein animation is done
          _master.getSceneLoader().fadeInCallback(destinationScene);
        }
      }

      // Outro modificator
      if (!blnTweenDone && blnIntroDone && !blnOutroDone)
      {
        fAlpha = Mathf.Lerp(1, 0, fCurrentDuration / fOutroDuration);
        if (fAlpha == 0)
        {
          // STEP 4: Fade out ends
          // Sends callback to parent gobject telling fadeout animation is done
          _master.getSceneLoader().fadeOutCallback();
        }
      }
    }
  }
}
