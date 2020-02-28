using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiperController : MonoBehaviour
{
    private GameObject currentCollision;
    private bool colliding = false;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SmudgeManager.WipeSmudge();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeJ);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeK);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SmudgeManager.SpraySmudge(Smudge.SmudgeType.smudgeL);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentCollision = other.gameObject;
        if (currentCollision.CompareTag("Smudge"))
        {
            colliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliding = false;
    }
}