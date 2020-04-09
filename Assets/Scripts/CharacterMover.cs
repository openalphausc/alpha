﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Controls the left-right movement of the character
// as well as finding the closest smudge to the player
public class CharacterMover : MonoBehaviour
{
    public float movementSpeed;
    public int speedState = 0; // 0 is free, 1 is slow
    public float timeCleaningUp;

    public static int closestSmudge = -1;
    public static Vector3 closestRelativePosition = Vector3.positiveInfinity;

    void Start()
    {
    }

    void Update()
    {
        if(speedState == 1) {
          movementSpeed = 0.2f;
          timeCleaningUp -= Time.deltaTime;
          Debug.Log("cleaning up " + timeCleaningUp);
          if(timeCleaningUp <= 0) speedState = 0;
        }
        else if(speedState == 0) movementSpeed = 4;

        if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(-movementSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(movementSpeed);
        }

        if(Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void MovePlayer(float distance)
    {
        transform.Translate(distance * Time.deltaTime, 0, 0);
        FindClosest();
    }

    // calculates the nearest smudge
    public void FindClosest()
    {
        Vector3 closestPosition = Vector3.positiveInfinity;
        int i = 0;
        foreach (Smudge smudge in FloorManager.currentFloor.smudgeManager.allSmudges)
        {
            Vector3 relative = smudge.transform.position - this.transform.position;
            if (Mathf.Abs(relative.x) < Mathf.Abs(closestPosition.x))
            {
                closestPosition = relative;
                closestSmudge = i;
            }

            i++;
        }

        closestRelativePosition = closestPosition;
    }
}