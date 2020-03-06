using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject WiperArmJoint;
    public GameObject SprayArmJoint;

    private WiperController wiperController;
    private SprayController sprayController;

    public float maxFluid = 4;
    public float fluidRemaining;

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

        if (Input.GetKeyDown(KeyCode.J))
        {
            sprayController.AnimateSpray(Smudge.SmudgeType.smudgeJ, (fluidRemaining > 0));
            if(fluidRemaining > 0) {
              SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeJ);
              fluidRemaining--;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            sprayController.AnimateSpray(Smudge.SmudgeType.smudgeK, (fluidRemaining > 0));
            if(fluidRemaining > 0) {
              SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeK);
              fluidRemaining--;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            sprayController.AnimateSpray(Smudge.SmudgeType.smudgeL, (fluidRemaining > 0));
            if(fluidRemaining > 0) {
              SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeL);
            }
        }

        if(Input.GetKeyDown(KeyCode.F) && fluidRemaining <= 0) {
          fluidRemaining = maxFluid;
          gaugeControl.transform.position = new Vector3(gaugeControl.transform.position.x, gaugeControl.startPos, gaugeControl.transform.position.z);
          gaugeControl.transform.localScale = new Vector3(gaugeControl.transform.localScale.x, gaugeControl.startScale, gaugeControl.transform.localScale.z);
        }
    }
}