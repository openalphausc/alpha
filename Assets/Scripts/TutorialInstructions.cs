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
    private float maxTimeOnScreen = 7f;
    private int currentBark = -1;

    public AudioSource radiosound;
    public AudioSource ahasound;

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
      bool startSounds = true;
      if(floor == 0 && currentBark == -1) currentBark = 0;
      else if(floor == 1 && !moving && currentBark == 3) currentBark = 4;
      else if(floor == 2 && !moving && currentBark == 4) currentBark = 5;
      else if(floor == 3 && !moving && currentBark == 5) currentBark = 6;
      else if(floor == 4 && !moving && currentBark == 6) currentBark = 7;
      else if(floor == 5 && !moving && currentBark == 14) currentBark = 15;
      else if(floor == 8 && !moving && currentBark == 15) currentBark = 16;
      else startSounds = false;
      if(startSounds) {
        ahasound.Play();
        radiosound.Play();
      }

      ShowBark(currentBark);
    }

    void ShowBark(int i) {
      if(barkStatus[i] == 0) {
        displayText.text = "";
        if(nextBark[i] != -1) currentBark = nextBark[i];
        else radiosound.Stop();
        return;
      }

      if(barkStatus[i] == 1) {
        if(i == 3) displayText.text = barks[i] + " >";
        else if(i == 4) displayText.text = barks[i] + " >A,D";
        else if(i == 6) displayText.text = barks[i] + " >J";
        else displayText.text = barks[i] + " >";
      }
      else displayText.text = barks[i];

      if(barkStatus[i] == 1 || barkStatus[i] == 2) Time.timeScale = 0f; // pause world
      // print("showing bark for currentBark = " + i);

      if(barkStatus[i] == 1 && (i == 4 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
      || i == 6 && Input.GetKeyDown(KeyCode.J) ||
      i != 4 && i != 6 && Input.GetKeyDown(KeyCode.Space)
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

      barks.Add("Now, wipe those smudges and get crackin'.");
      barkStatus.Add(3); nextBark.Add(-1);

      barks.Add("To spray smudges, press 'J'.");
      barkStatus.Add(1); nextBark.Add(-1);

      barks.Add("'Fore I forget... you see at the bottom of your screen?");
      barkStatus.Add(1); nextBark.Add(8);
      barks.Add("When you spray, you use up my cleaning fluid.");
      barkStatus.Add(1); nextBark.Add(9);
      barks.Add("But with my patented Fast-Fill...");
      barkStatus.Add(1); nextBark.Add(10);
      barks.Add("...it'll automatically start refilling when it's empty.");
      barkStatus.Add(1); nextBark.Add(11);
      barks.Add("Just make sure you press 'J' to cap the bottle...");
      barkStatus.Add(1); nextBark.Add(12);
      barks.Add("...and stop it from overflowin'! I don't want you makin' a mess.");
      barkStatus.Add(1); nextBark.Add(13);
      barks.Add("If you don't cap it in time, you'll have to stop wiping to clean it up.");
      barkStatus.Add(1); nextBark.Add(14);
      barks.Add("And that means less profit for me! SO DON'T DO IT!");
      barkStatus.Add(1); nextBark.Add(-1);

      barks.Add("There! It's fillin' up! Don't let it overflow!"); // 15
      barkStatus.Add(3); nextBark.Add(-1);

      barks.Add("EH? What's that? More smudges on this building?"); // 16
      barkStatus.Add(1); nextBark.Add(17);
      barks.Add("Alright, alright! I've got just the thing. Lemme...just... find it.");
      barkStatus.Add(1); nextBark.Add(18);
      barks.Add("AH! Here ya go, Squeegee. Two new fluids to deal with those smudges!");
      barkStatus.Add(1); nextBark.Add(19);
      barks.Add("You're still new at this, so LISTEN UP!");
      barkStatus.Add(1); nextBark.Add(20);
      barks.Add("Press 'K' to spray and cap the yellow fluid and 'L' for the green one.");
      barkStatus.Add(1); nextBark.Add(21);
      barks.Add("And watch out: this green one looks like it's growin'! Get that one first!");
      barkStatus.Add(1); nextBark.Add(22);
      barks.Add("Remember: don't let 'em overflow! And keep track of your fluid supplies!");
      barkStatus.Add(3); nextBark.Add(23);
      barks.Add("You don't wanna stand around wasting time 'n waiting for one to refill.");
      barkStatus.Add(3); nextBark.Add(24);
      barks.Add("TIME IS MONEY, SQUEEGEE!");
      barkStatus.Add(3); nextBark.Add(25);
      barks.Add("You better finish before sundown! Or else it's comin' out of your paychceck!");
      barkStatus.Add(3); nextBark.Add(26);
      barks.Add("**RADIO STATIC**");
      barkStatus.Add(3); nextBark.Add(-1);
    }
}
