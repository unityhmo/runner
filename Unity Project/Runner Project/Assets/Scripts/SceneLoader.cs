using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
  private GameObject fadeInFadeOut;

  public void goToScene(int sceneIndex)
  {
    if (fadeInFadeOut == null)
    {
      fadeInFadeOut = ObjectFactory.createFadeInFadeOut();
      fadeInFadeOut.GetComponent<FadeInFadeOut>().fadeIn(sceneIndex);
    }
  }

  public void fadeInCallback(int sceneIndex)
  {
    StartCoroutine(loadNewScene(sceneIndex));
  }

  public void fadeOutCallback()
  {
    Destroy(fadeInFadeOut);
    fadeInFadeOut = null;

    /*
    Sends a broadcast to all gameobjects in ROOT level, just to let them know fadeIn cycle is done.
    If you want something to happens at the begging of a scene, add 'fadeOutDone' function
    in a root gameobject.
    */
    GameObject[] gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
    foreach (GameObject go in gos)
    {
      if (go && go.transform.parent == null)
      {
        go.gameObject.SendMessage("fadeOutFinished", null, SendMessageOptions.DontRequireReceiver);
      }
    }
  }

  IEnumerator loadNewScene(int sceneIndex)
  {
    AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);

    while (!async.isDone)
    {
      yield return null;
    }

    if (async.isDone)
      fadeInFadeOut.GetComponent<FadeInFadeOut>().fadeOut();
  }
}
