using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// controls the wiper arm
public class WiperController : ArmController
{
    [SerializeField] private float passiveReachRatio; // percentage to reach towards nearest target

    public AudioSource wipe1;
    public AudioSource wipe2;
    public AudioSource wipe3;


    protected override void Start()
    {
        base.Start();
        // source_ = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (CharacterMover.targeting && !animating)
        {
            PassiveWipe();
        }
        else if (!animating)
        {
            RestArm();
        }
    }

    // constantly point at nearest target
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

    // begin wiping animation
    public void AnimateWipe()
    {
        if (animating)
        {
            // animation cancel
            StopCoroutine(coroutine);
        }

        // aim the spraying arm
        Vector3 closest = ClosestRelativeToArm();

        StretchArm(closest.magnitude);
        transform.LookAt(closest + transform.position);

        // perform wipe
        if (FloorManager.currentFloor.smudgeManager.WipeSmudge())
        {
            int choice = Random.Range(1, 4);
            if(choice == 1) wipe1.Play();
            else if(choice == 2) wipe2.Play();
            else if(choice == 3) wipe3.Play();
        }

        coroutine = FinishWipe();
        StartCoroutine(coroutine);
    }

    // after delay, retract arm
    private IEnumerator FinishWipe()
    {
        animating = true;
        yield return new WaitForSeconds(0.5f);
        animating = false;
    }
}
