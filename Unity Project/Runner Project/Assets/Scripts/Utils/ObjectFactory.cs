using UnityEngine;

// Static class with specific gameObjec build up functions.
public static class ObjectFactory
{
  /*
   * Here we create our glorious GameMaster gameobject with necesary behavior components and a descent name.
   * Note that we return GameMaster monobehavior component as the main scope for this element.
   */
  public static GameMaster CreateGameMaster()
  {
    GameObject master = new GameObject();
    master.AddComponent<SceneLoader>();
    master.name = BaseValues.LABEL_GAME_MASTER;

    return master.AddComponent<GameMaster>();
  }

  /*
   * FadeInFadeOut gameobject builder, same as above, but this time we return the actual GameObject as scope
   */
  public static GameObject CreateFadeInFadeOut()
  {
    GameObject fadeInFadeOut = new GameObject();
    fadeInFadeOut.AddComponent<FadeInFadeOut>();
    fadeInFadeOut.name = BaseValues.LABEL_FADEIN_FADEOUT;

    return fadeInFadeOut;
  }
}
