using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// attached to Character, processes non-movement controls and manages fluid levels
public class InputHandler : MonoBehaviour
{
    public GameObject WiperArmJoint;
    public GameObject SprayArmJoint;

    private WiperController wiperController;
    private SprayController sprayController;

    public List<float> fluidRemaining;
    public List<bool> refilling;
    public float maxFluid = 4f;

    public GameObject gaugeJ;
    public GameObject gaugeK;
    public GameObject gaugeL;
    private GaugeMove gaugeMoveJ;
    private GaugeMove gaugeMoveK;
    private GaugeMove gaugeMoveL;

    public GameObject character;
    private CharacterMover characterMover;

    public AudioSource capsound;

    void Start()
    {
        wiperController = WiperArmJoint.GetComponent<WiperController>();
        sprayController = SprayArmJoint.GetComponent<SprayController>();
        maxFluid += PersistentManagerScript.Instance.InvSprayIncrease();
        fluidRemaining.Clear();
        for(int i = 0; i < 3; i++) {
          fluidRemaining.Add(maxFluid);
          refilling.Add(false);
        }

        gaugeMoveJ = gaugeJ.GetComponent<GaugeMove>();
        gaugeMoveK = gaugeK.GetComponent<GaugeMove>();
        gaugeMoveL = gaugeL.GetComponent<GaugeMove>();

        characterMover = character.GetComponent<CharacterMover>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CharacterMover.targeting)
        {
            wiperController.AnimateWipe();
        }

        // Loop through J, K, L and do keydown things
        for(int i = 0; i < 3; i++) {
          // set up variables
          Smudge.SmudgeType spray = Smudge.SmudgeType.SmudgeJ;
          if(i == 0 && !Input.GetKeyDown(KeyCode.J)) continue;
          else if(i == 1) {
            if(!Input.GetKeyDown(KeyCode.K)) continue;
            spray = Smudge.SmudgeType.SmudgeK;
          }
          else if(i == 2) {
            if(!Input.GetKeyDown(KeyCode.L)) continue;
            spray = Smudge.SmudgeType.SmudgeL;
          }
          // key is down

          // if not refilling or cleaning up, attempt to spray
          if(!refilling[i] && characterMover.speedState == 0) {
            Spray(i);
          }
          // if refilling and above a certain point, stop refilling
          if(refilling[i] && fluidRemaining[i] >= 1) {
            refilling[i] = false;
            capsound.Play();
            if(SmudgeManager.currentTarget == -1) continue; // don't spray if not targeting anything
            Smudge.SmudgeType target = FloorManager.currentFloor.smudgeManager.allSmudges[SmudgeManager.currentTarget].type;
            if(target == spray) Spray(i);
          }
        }
    }

    void Spray(int fluidIndex) {
      Smudge.SmudgeType spray = Smudge.SmudgeType.SmudgeJ;
      if(fluidIndex == 1) spray = Smudge.SmudgeType.SmudgeK;
      else if(fluidIndex == 2) spray = Smudge.SmudgeType.SmudgeL;
      bool inRange = sprayController.AnimateSpray(spray, (fluidRemaining[fluidIndex] > 0));
      if(fluidRemaining[fluidIndex] > 0) {
        FloorManager.currentFloor.smudgeManager.SpraySmudge(spray);
        fluidRemaining[fluidIndex]--;
        if(fluidIndex == 0) gaugeMoveJ.decreasing = true;
        else if(fluidIndex == 1) gaugeMoveK.decreasing = true;
        else if(fluidIndex == 2) gaugeMoveL.decreasing = true;
      }
      // Debug.Log("After spraying, fluid of " + fluidIndex + " is " + fluidRemaining[fluidIndex]);
    }
}
