using UnityEngine;

public class FadeInFadeOut : MonoBehaviour
{
  private GameMaster master;
  private string destinationScene;
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
    master = GameMaster.getInstance();
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

  public void fadeIn(string sceneName)
  {
    destinationScene = sceneName;
    fCurrentDuration = 0f;
    blnStartAnimation = true;
  }

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
          blnTweenDone = true;
          blnIntroDone = true;
          // Sends callback to parent gobject telling fadein animation is done
          master.fadeInCallback(destinationScene);
        }
      }

      // Outro modificator
      if (!blnTweenDone && blnIntroDone && !blnOutroDone)
      {
        fAlpha = Mathf.Lerp(1, 0, fCurrentDuration / fOutroDuration);
        if (fAlpha == 0)
        {
          // Sends callback to parent gobject telling fadeout animation is done
          master.fadeOutCallback();
        }
      }
    }
  }
}
