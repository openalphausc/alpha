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
        Vector3 closestPosition = Vector3.positiveInfinity;
        int closestSmudge = -1;
        foreach (Smudge smudge in SmudgeManager.allSmudges)
        {
            Vector3 relative = smudge.transform.position - this.transform.position;
            if (Mathf.Abs(relative.x) < Mathf.Abs(closestPosition.x))
            {
                closestPosition = relative;
                closestSmudge = smudge.Index;
            }
        }

        if (closestPosition.magnitude <= targetRange)
        {
            // transform.LookAt(closestPosition + this.transform.position);
            SmudgeManager.SelectSmudge(closestSmudge);
        }
        else
        {
            transform.rotation = Quaternion.identity;
            SmudgeManager.DeselectSmudge();
        }

        if (closestPosition.magnitude <= maxArmLength)
        {
            StretchArm(closestPosition.magnitude);
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