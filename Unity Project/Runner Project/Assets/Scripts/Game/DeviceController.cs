using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Component in charge of only reading user's input and sending them to PlayerController
public class DeviceController
{

  public bool isTablet() {

    // Aspect Ratio equals or larger than 3:4 (some tablets, ipads)
    if(GetAspectRatio() >= 0.75) {
      return true;
    } else {
      return false;
    }

  }

  private float GetAspectRatio()
  {
    return (float)Screen.width / (float)Screen.height;
  }

}

