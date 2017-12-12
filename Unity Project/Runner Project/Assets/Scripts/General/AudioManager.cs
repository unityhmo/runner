using UnityEngine;
using System.Collections.Generic;

public static class AudioManager
{

  private static AudioUserInterface _uiFx;
  private static AudioMusic _mFx;
  private static CharacterSFX _charFx;
  private static MenuScenarioSFX _menuScenarioFx;

  /*
   * The following functions are getters and setters
   * of script components that handles AudioSource 
   * behaviour for the whole game. Such as menus,
   * character and global audio managers (UI and music)
   */
  public static void SetUIFX(AudioUserInterface uiFx)
  {
    _uiFx = uiFx;
  }

  public static AudioUserInterface GetUIFX()
  {
    return _uiFx;
  }

  public static void SetMFX(AudioMusic mFx)
  {
    _mFx = mFx;
  }

  public static AudioMusic GetMFX()
  {
    return _mFx;
  }

  public static void SetCharFX(CharacterSFX charFx)
  {
    _charFx = charFx;
  }

  public static CharacterSFX GetCharFX()
  {
    return _charFx;
  }

  public static void SetMenuScenarioFX(MenuScenarioSFX menuScenarioFx)
  {
    _menuScenarioFx = menuScenarioFx;
  }

  public static MenuScenarioSFX GetMenuScenarioFX()
  {
    return _menuScenarioFx;
  }
}
