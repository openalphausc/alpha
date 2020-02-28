using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiperController : MonoBehaviour
{
    public GameObject armJoint;

    public bool colliding = false;

    private GameObject currentCollision;
    private ArmController armController;


    void Start()
    {
        armController = armJoint.GetComponent<ArmController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            armController.AnimateWipe();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            armController.AnimateSpray(Smudge.SmudgeType.smudgeJ);
            SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeJ);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            armController.AnimateSpray(Smudge.SmudgeType.smudgeK);
            SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeK);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            armController.AnimateSpray(Smudge.SmudgeType.smudgeL);
            SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeL);
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