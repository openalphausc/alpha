using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialInstructions : MonoBehaviour
{
    public Text displayText;

    // Start is called before the first frame update
    void Start()
    {
      displayText = GetComponent<Text>();
      displayText.text = "PRESS SPACE";
    }

    // Update is called once per frame
    void Update()
    {
      int floor = FloorManager.floorIndex;
      bool moving = FloorManager.moving;
      string text = "";
      if(floor == 0) text = "PRESS SPACE";
      else if(floor == 1 && moving) text = "GOOD";
      else if(floor == 1 && !moving) text = "PRESS A AND D TO MOVE";
      else if(floor == 2 && moving) text = "WELL DONE";
      else if(floor == 2 && !moving) text = "CLEAN ALL 3 SMUDGES";
      else if(floor == 3 && !moving) text = "SPRAY CLEANING FLUID WITH J";
      else if(floor == 4 && !moving) text = "FLUID WILL START REFILLING AUTOMATICALLY";
      else if(floor == 5 && moving) text = "YOUR CLEANING FLUID IS REFILLING.";
      else if(floor == 5 && !moving) text = "WHEN IT'S CLOSE TO FULL, STOP IT WITH J";
      else if(floor == 6 && moving) text = "IF YOU LET YOUR FLUID OVERFLOW,";
      else if(floor == 6 && !moving) text = "YOU'LL HAVE TO STOP AND CLEAN UP THE MESS";
      else if(floor == 7 && !moving) text = "NOW FINISH THE BUILDING";
      displayText.text = text;
    }
}
