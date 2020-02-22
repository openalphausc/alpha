using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public float maxArmLength;
    public float targetRange;

    private Transform armTransform;
    private Transform handTransform;

    void Start()
    {
        armTransform = GameObject.Find("Arm").transform;
        handTransform = GameObject.Find("Hand").transform;
    }

    void Update()
    {
        // find relative position of closest smudge
        Vector3 closest = Vector3.positiveInfinity;
        foreach (GameObject smudge in SmudgeManager.allSmudges)
        {
            Vector3 relative = smudge.transform.position - this.transform.position;
            if (relative.magnitude < closest.magnitude)
            {
                closest = relative;
            }
        }

        if (closest.magnitude <= targetRange)
        {
            transform.LookAt(closest + this.transform.position);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }

        if (closest.magnitude <= maxArmLength)
        {
            StretchArm(closest.magnitude);
        }
        else
        {
            StretchArm(maxArmLength);
        }
    }

    void StretchArm(float length)
    {
        handTransform.localPosition = length * Vector3.forward;
        armTransform.localPosition = length / 2 * Vector3.forward;

        Vector3 localScale = armTransform.localScale;
        localScale = new Vector3(localScale.x, localScale.y, length);
        armTransform.localScale = localScale;
    }

}