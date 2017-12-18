using System.Collections.Generic;
using System.Collections;

// Static class with the sole responsability of returning a list of X stages we decide to add.
public static class Stages
{
  public static List<Stage> BuildStages(GameDataInfo dataInfo)
  {
    // Note that we could skip adding some values, each stage will take its base value at Stage class.
    List<Stage> stages = new List<Stage>();
    Stage newStage;

    newStage = new Stage();
    newStage.SetConfig(0, (Hashtable)dataInfo.StageConfig)
      .SetAssetPath("level_0")
      .SetLabel("Hello World");
    stages.Add(newStage);

    newStage = new Stage();
    newStage.SetConfig(1, (Hashtable)dataInfo.StageConfig)
      .SetAssetPath("level_1")
      .SetLabel("The Short Path");
    stages.Add(newStage);

    newStage = new Stage();
    newStage.SetConfig(2, (Hashtable)dataInfo.StageConfig)
      .SetAssetPath("level_2")
      .SetLabel("The Long One");
    stages.Add(newStage);

    newStage = new Stage();
    newStage.SetConfig(3, (Hashtable)dataInfo.StageConfig)
        .SetAssetPath("level_3")
        .SetLabel("An easy one");
    stages.Add(newStage);

    newStage = new Stage();
    newStage.SetConfig(4, (Hashtable)dataInfo.StageConfig)
        .SetAssetPath("level_4")
        .SetLabel("Don't jump!");
    stages.Add(newStage);

    return stages;
  }
}
