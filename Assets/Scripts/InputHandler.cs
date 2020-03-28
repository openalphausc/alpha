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

    public float maxFluid = 4;
    public float fluidRemaining;
    public bool refilling = false;

    public GameObject gaugeFront;
    private GaugeControl gaugeControl;


    void Start()
    {
        wiperController = WiperArmJoint.GetComponent<WiperController>();
        sprayController = SprayArmJoint.GetComponent<SprayController>();

        fluidRemaining = maxFluid;
        gaugeControl = gaugeFront.GetComponent<GaugeControl>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            wiperController.AnimateWipe();
        }

        if (Input.GetKeyDown(KeyCode.J) && !refilling)
        {
            sprayController.AnimateSpray(Smudge.SmudgeType.SmudgeJ, (fluidRemaining > 0));
            if(fluidRemaining > 0) {
                FloorManager.currentFloor.smudgeManager.SpraySmudge(Smudge.SmudgeType.SmudgeJ);
              fluidRemaining--;
            }
        }

        if (Input.GetKeyDown(KeyCode.K) && !refilling)
        {
            sprayController.AnimateSpray(Smudge.SmudgeType.SmudgeK, (fluidRemaining > 0));
            if(fluidRemaining > 0) {
                FloorManager.currentFloor.smudgeManager.SpraySmudge(Smudge.SmudgeType.SmudgeK);
              fluidRemaining--;
            }
        }

        if (Input.GetKeyDown(KeyCode.L) && !refilling)
        {
            sprayController.AnimateSpray(Smudge.SmudgeType.SmudgeL, (fluidRemaining > 0));
            if(fluidRemaining > 0) {
                FloorManager.currentFloor.smudgeManager.SpraySmudge(Smudge.SmudgeType.SmudgeL);
              fluidRemaining--;
            }
        }

        if(Input.GetKeyDown(KeyCode.F) && fluidRemaining <= 0) {
          refilling = true;
          // gaugeControl.transform.position = new Vector3(gaugeControl.transform.position.x, gaugeControl.startPos, gaugeControl.transform.position.z);
          // gaugeControl.transform.localScale = new Vector3(gaugeControl.transform.localScale.x, gaugeControl.startScale, gaugeControl.transform.localScale.z);
        }
    }
}