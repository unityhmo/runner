using System.Collections.Generic;

// Static class with the sole responsability of returning a list of X stages we decide to add.
public static class Stages
{
  public static List<Stage> buildStages(GameDataInfo dataInfo)
  {
    // Note that we could skip adding some values, each stage will take its base value at Stage class.
    List<Stage> stages = new List<Stage>();
    Stage newStage;

    newStage = new Stage();
    newStage.setIslocked((bool)dataInfo.statesLocked["level_0"])
      .setAssetPath("level_0")
      .setLabel("Hello World")
      .setTotalPowerUps(1);
    stages.Add(newStage);

    newStage = new Stage();
    newStage.setIslocked((bool)dataInfo.statesLocked["level_1"])
      .setAssetPath("level_1")
      .setLabel("The Long Path")
      .setTotalPowerUps(9);
    stages.Add(newStage);

    newStage = new Stage();
    newStage.setIslocked((bool)dataInfo.statesLocked["level_2"])
      .setAssetPath("level_2")
      .setLabel("Final Stage")
      .setTotalPowerUps(3);
    stages.Add(newStage);

    return stages;

    // TODO: Setting up total powerups is lame, we should find a automatic human-error-free method.
  }
}
