using UnityEngine;

public static class BaseValues
{
  public static string TAG_PLAYER
  {
    get
    {
      return "Player";
    }
  }

  public static string TAG_MENU_CONTROLLER
  {
    get
    {
      return "MenuController";
    }
  }

  public static string TAG_GAME_CONTROLLER
  {
    get
    {
      return "GameController";
    }
  }

  public static string TAG_STAGE_CONTROLLER
  {
    get
    {
      return "StageController";
    }
  }

  public static string TAG_STAGE_BLOCK
  {
    get
    {
      return "StageBlock";
    }
  }

  public static string TAG_ORB_PICKUP
  {
    get
    {
      return "Orb";
    }
  }

  public static string TAG_OBSTACLE
  {
    get
    {
      return "Obstacle";
    }
  }

  public static string TAG_GOAL
  {
    get
    {
      return "Goal";
    }
  }

  public static string LABEL_GAME_MASTER
  {
    get
    {
      return "GameMaster";
    }
  }

  public static string LABEL_UI_AUDIO
  {
    get
    {
      return "UIAudio";
    }
  }

  public static string LABEL_MUSIC_AUDIO
  {
    get
    {
      return "MusicAudio";
    }
  }

  public static string LABEL_FADEIN_FADEOUT
  {
    get
    {
      return "FadeInFadeOut";
    }
  }

  public static string LABEL_STAGE_BLOCKS_RESULT
  {
    get
    {
      return "stage_layout";
    }
  }

  public static string PATH_AUDIO_RESOURCES
  {
    get
    {
      return "Audio/";
    }
  }

  public static string PATH_STAGES_RESOURCES
  {
    get
    {
      return "Stages/";
    }
  }

  public static string RECEIVER_FADEOUT_FINISHED
  {
    get
    {
      return "FadeOutFinished";
    }
  }

  public static string RECEIVER_COLLISION_DETECTED
  {
    get
    {
      return "CollisionDetected";
    }
  }

  public static int GetStars(int user, int total)
  {
    int stars = 0;
    int percent = 0;

    if (user > 0 && total > 0)
    {
      percent = (int)Mathf.Round(((float)user / (float)total) * 100f);

      if (percent == 100)
        stars = 3;
      else if (percent > 70)
        stars = 2;
      else if (percent > 40)
        stars = 1;
    }

    return stars;
  }
}
