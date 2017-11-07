using System.Collections.Generic;

public static class Stages
{
  public static List<Stage> buildStages()
  {
    List<Stage> stages = new List<Stage>();
    Stage newStage;

    newStage = new Stage();
    newStage.setIslocked(false)
      .setAssetPath("level_0")
      .setLabel("Hello World")
      .setTotalPowerUps(1);
    stages.Add(newStage);

    newStage = new Stage();
    newStage.setAssetPath("level_1")
      .setLabel("The Long Path")
      .setTotalPowerUps(9);
    stages.Add(newStage);

    newStage = new Stage();
    newStage.setAssetPath("level_2")
      .setLabel("Final Stage")
      .setTotalPowerUps(3);
    stages.Add(newStage);

    return stages;
  }
}
