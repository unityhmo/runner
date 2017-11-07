using UnityEngine;

public static class ObjectFactory
{
  public static GameMaster createGameMaster()
  {
    GameObject master = new GameObject();
    master.AddComponent<SceneLoader>();
    master.name = "GameMaster";

    return master.AddComponent<GameMaster>();
  }

  public static GameObject createFadeInFadeOut()
  {
    GameObject fadeInFadeOut = new GameObject();
    fadeInFadeOut.AddComponent<FadeInFadeOut>();
    fadeInFadeOut.name = "FadeInFadeOut";

    return fadeInFadeOut;
  }
}
