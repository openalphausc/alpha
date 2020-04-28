using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialInstructions : MonoBehaviour
{
    public TextMeshProUGUI displayText;

    private List<string> barks; // index is floor for both lists
    private List<float> barkStatus; // 0 = done and gone, 1 = pause and waiting for keypress, 2 = pause and timing out soon, 3+ = not paused and timing out soon
    private List<int> nextBark;
    private float maxTimeOnScreen = 5f;
    private int currentBark = 0;


    // Start is called before the first frame update
    void Start()
    {
      displayText = GetComponent<TextMeshProUGUI>();
      displayText.text = "INSTRUCTIONS HERE";

      barks = new List<string>();
      barkStatus = new List<float>();
      nextBark = new List<int>();

      LoadBarks();
    }

    // Update is called once per frame
    void Update()
    {
      int floor = FloorManager.floorIndex;
      bool moving = FloorManager.moving;
      if(floor == 1 && !moving) currentBark = 4;
      if(floor == 2 && !moving) currentBark = 5;
      if(floor == 3 && !moving) currentBark = 6;


      ShowBark(currentBark);
    }

    void ShowBark(int i) {
      if(barkStatus[i] == 0) {
        displayText.text = "";
        if(nextBark[i] != -1) currentBark = nextBark[i];
        return;
      }

      displayText.text = barks[i];
      if(barkStatus[i] == 1 || barkStatus[i] == 2) Time.timeScale = 0f; // pause world
      print("showing bark for currentBark = " + i);

      if(barkStatus[i] == 1 && (i == 3 && Input.GetKeyDown(KeyCode.Space)
      || i == 4 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
      || i == 6 && Input.GetKeyDown(KeyCode.J)
      )) {
        barkStatus[i] = 0;
        Time.timeScale = 1f; // unpause world
      }
      else if(barkStatus[i] >= 2) {
        barkStatus[i] += Time.unscaledDeltaTime;
        if(barkStatus[i] > maxTimeOnScreen) {
          Time.timeScale = 1f; // unpause world
          barkStatus[i] = 0;
        }
      }
    }

    void LoadBarks() {
      barks.Add("You're real new to this, huh?");
      barkStatus.Add(2); nextBark.Add(1);
      barks.Add("You know what? I think I'll call you Squeegee.");
      barkStatus.Add(2); nextBark.Add(2);
      barks.Add("ALRIGHT! Listen up 'cause I only want to explain this once.");
      barkStatus.Add(2); nextBark.Add(3);
      barks.Add("You see those disgusting smudges? Press 'SPACE' to wipe 'em.");
      barkStatus.Add(1); nextBark.Add(-1);

      barks.Add("You want to move? Press 'A' and 'D'.");
      barkStatus.Add(1); nextBark.Add(-1);

      barks.Add("Now, spray those smudges and get crackin'.");
      barkStatus.Add(3); nextBark.Add(-1);

      barks.Add("To spray smudges, press 'J'.");
      barkStatus.Add(1); nextBark.Add(-1);
    }
}
