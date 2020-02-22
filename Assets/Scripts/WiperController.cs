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
        if (Input.GetKeyDown(KeyCode.Space) && colliding)
        {
            SmudgeManager.RemoveSmudge(currentCollision);
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
