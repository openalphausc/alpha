using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiperController : MonoBehaviour
{
    private GameObject currentCollision;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentCollision.CompareTag("Smudge"))
        {
            SmudgeManager.allSmudges.Remove(currentCollision);
            Destroy(currentCollision);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentCollision = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        currentCollision = this.gameObject;
    }
}
