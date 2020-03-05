using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiperController : MonoBehaviour
{
    public GameObject armJoint;

    public GameObject gaugeFront;
    private GaugeControl gaugeControl;

    public bool colliding = false;

    private GameObject currentCollision;
    private ArmController armController;

    public float maxFluid = 4;
    public float fluidRemaining;


    void Start()
    {
        armController = armJoint.GetComponent<ArmController>();
        fluidRemaining = maxFluid;

        gaugeControl = gaugeFront.GetComponent<GaugeControl>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            armController.AnimateWipe();
        }


        if (Input.GetKeyDown(KeyCode.J))
        {
            armController.AnimateSpray(Smudge.SmudgeType.smudgeJ, (fluidRemaining > 0));
            if(fluidRemaining > 0) SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeJ);
            fluidRemaining--;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            armController.AnimateSpray(Smudge.SmudgeType.smudgeK, (fluidRemaining > 0));
            if(fluidRemaining > 0) SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeK);
            fluidRemaining--;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            armController.AnimateSpray(Smudge.SmudgeType.smudgeL, (fluidRemaining > 0));
            if(fluidRemaining > 0) SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeL);
            fluidRemaining--;
        }

        if(fluidRemaining <= 0 && Input.GetKeyDown(KeyCode.R)) {
          fluidRemaining = maxFluid;
          gaugeControl.transform.position = new Vector3(gaugeControl.transform.position.x, gaugeControl.startPos, gaugeControl.transform.position.z);
          gaugeControl.transform.localScale = new Vector3(gaugeControl.transform.localScale.x, gaugeControl.startScale, gaugeControl.transform.localScale.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentCollision = other.gameObject;
        if (currentCollision.CompareTag("Smudge"))
        {
            colliding = true;
            if (armController.animationState == ArmController.AnimationState.wiping)
            {
                SmudgeManager.WipeSmudge();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliding = false;
    }
}