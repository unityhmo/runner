using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMaster : MonoBehaviour
{
  protected static GameMaster _instance;
  private GameObject fadeInFadeOut;

  void Awake()
  {
    DontDestroyOnLoad(transform.gameObject);
    if (_instance != null)
      DestroyObject(gameObject);
    else
      _instance = this;
  }

  public static GameMaster getInstance()
  {
    if (_instance == null)
    {
      GameObject master = new GameObject();
      master.name = "GameMaster";

      _instance = master.AddComponent<GameMaster>();
    }

    return _instance;
  }

  public void goToScene(string sceneName)
  {
    if (fadeInFadeOut == null)
    {
      fadeInFadeOut = new GameObject();
      fadeInFadeOut.AddComponent<FadeInFadeOut>().fadeIn(sceneName);
      fadeInFadeOut.name = "FadeInFadeOut";
    }
  }

  public void fadeInCallback(string sceneName)
  {
    StartCoroutine(loadNewScene(sceneName));
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

  IEnumerator loadNewScene(string sceneName)
  {
    AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

    while (!async.isDone)
    {
      yield return null;
    }

    if (async.isDone)
      fadeInFadeOut.GetComponent<FadeInFadeOut>().fadeOut();
  }
}
