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


    void Start()
    {
        wiperController = WiperArmJoint.GetComponent<WiperController>();
        sprayController = SprayArmJoint.GetComponent<SprayController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            wiperController.AnimateWipe();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            sprayController.AnimateSpray(Smudge.SmudgeType.smudgeJ);
            SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeJ);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            sprayController.AnimateSpray(Smudge.SmudgeType.smudgeK);
            SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeK);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            sprayController.AnimateSpray(Smudge.SmudgeType.smudgeL);
            SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeL);
        }
    }
}