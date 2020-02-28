using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float movementSpeed;

    public static int closestSmudge = -1;
    public static Vector3 closestRelativePosition = Vector3.positiveInfinity;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(-movementSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(movementSpeed);
        }
    }

    void MovePlayer(float distance)
    {
        transform.Translate(distance * Time.deltaTime, 0, 0);
        FindClosest();
    }

    public void FindClosest()
    {
        Vector3 closestPosition = Vector3.positiveInfinity;
        int i = 0;
        foreach (Smudge smudge in SmudgeManager.allSmudges)
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