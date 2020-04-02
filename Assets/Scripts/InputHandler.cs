using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject gaugeFront;
    private GaugeControl gaugeControl;


    void Start()
    {
        wiperController = WiperArmJoint.GetComponent<WiperController>();
        sprayController = SprayArmJoint.GetComponent<SprayController>();

        fluidRemaining.Clear();
        for(int i = 0; i < 3; i++) {
          fluidRemaining.Add(maxFluid);
          refilling.Add(false);
        }

        gaugeControl = gaugeFront.GetComponent<GaugeControl>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            wiperController.AnimateWipe();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
          if(!refilling[0]) {
            sprayController.AnimateSpray(Smudge.SmudgeType.SmudgeJ, (fluidRemaining[0] > 0));
            if(fluidRemaining[0] > 0) {
                FloorManager.currentFloor.smudgeManager.SpraySmudge(Smudge.SmudgeType.SmudgeJ);
              fluidRemaining[0]--;
            }
          }
          else if(fluidRemaining[0] <= 0) refilling[0] = true;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
          if(!refilling[1]) {
            sprayController.AnimateSpray(Smudge.SmudgeType.SmudgeK, (fluidRemaining[1] > 0));
            if(fluidRemaining[1] > 0) {
                FloorManager.currentFloor.smudgeManager.SpraySmudge(Smudge.SmudgeType.SmudgeK);
              fluidRemaining[1]--;
            }
          }
          else if(fluidRemaining[1] <= 0) refilling[1] = true;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
          if(!refilling[2]) {
            sprayController.AnimateSpray(Smudge.SmudgeType.SmudgeL, (fluidRemaining[2] > 0));
            if(fluidRemaining[2] > 0) {
                FloorManager.currentFloor.smudgeManager.SpraySmudge(Smudge.SmudgeType.SmudgeL);
              fluidRemaining[2]--;
            }
          }
          else if(fluidRemaining[2] <= 0) refilling[2] = true;
        }
    }
}