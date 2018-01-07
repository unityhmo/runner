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
	
	string[] names = {
		"Hello World",
		"The Short Path",
		"The Long One",
		"An easy one",
		"Don't jump!",
		"Long & Fast",
		"Zig Zag",
		"One Long Line",
		"Run Free",
		"Impossible"
	};
	
	for (int cnt = 0; cnt < names.Length; cnt++)
	{
		newStage = new Stage();
		
		newStage.SetConfig(cnt, (Hashtable)dataInfo.StageConfig)
			.SetAssetPath("level_" + cnt)
			.SetLabel(names[cnt]);
		
		stages.Add(newStage);
	}

    return stages;
  }
}
