using System.Collections.Generic;

// Static class with the sole responsability of returning a list of X stages we decide to add.
public static class Stages
{
  public static List<Stage> BuildStages(GameDataInfo dataInfo)
  {
    // Note that we could skip adding some values, each stage will take its base value at Stage class.
    List<Stage> stages = new List<Stage>();
    Stage newStage;

    newStage = new Stage();
    newStage.SetIslocked((bool)dataInfo.StateIsLocked["level_0"])
      .SetAssetPath("level_0")
      .SetLabel("Hello World");
    stages.Add(newStage);

    newStage = new Stage();
    newStage.SetIslocked((bool)dataInfo.StateIsLocked["level_1"])
      .SetAssetPath("level_1")
      .SetLabel("The Long Path");
    stages.Add(newStage);

    newStage = new Stage();
    newStage.SetIslocked((bool)dataInfo.StateIsLocked["level_2"])
      .SetAssetPath("level_2")
      .SetLabel("Final Stage");
    stages.Add(newStage);

    return stages;
  }
}
