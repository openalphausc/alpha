using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiperController : ArmController
{
    [SerializeField] private float wipeRange;
    [SerializeField] private float passiveReachRatio;


    protected  override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (ClosestRelativeToArm().magnitude <= targetRange && !animating)
        {
            PassiveWipe();
        }
    }

    private void PassiveWipe()
    {
        Vector3 closest = ClosestRelativeToArm();
        transform.LookAt(closest + transform.position);
        if (closest.magnitude <= maxArmLength / passiveReachRatio)
        {
            StretchArm(closest.magnitude * passiveReachRatio);
        }
        else
        {
            StretchArm(maxArmLength);
        }
    }

    public void AnimateWipe()
    {
        if (animating)
        {
            StopCoroutine(coroutine);
        }

        Vector3 closest = ClosestRelativeToArm();
        transform.LookAt(closest + transform.position);
        if (closest.magnitude <= maxArmLength)
        {
            StretchArm(closest.magnitude);
        }
        else
        {
            StretchArm(maxArmLength);
        }

        if (closest.magnitude <= wipeRange)
        {
            FloorManager.currentFloor.smudgeManager.WipeSmudge();
        }
        coroutine = FinishWipe();
        StartCoroutine(coroutine);
    }

    private IEnumerator FinishWipe()
    {
        animating = true;
        yield return new WaitForSeconds(0.5f);
        animating = false;
    }
}
