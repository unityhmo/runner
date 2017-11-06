using UnityEngine;

public static class GameMasterFactory
{
  public static GameMaster createGameMaster()
  {
    GameObject master = new GameObject();
    master.name = "GameMaster";

    return master.AddComponent<GameMaster>();
  }
}
