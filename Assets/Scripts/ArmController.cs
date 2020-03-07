using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Superclass for WiperController and SprayController
public abstract class ArmController : MonoBehaviour
{
    [SerializeField] protected float targetRange; // how close a smudge must be to be targeted
    [SerializeField] protected float maxArmLength; // maximum distance player can reach
    [SerializeField] protected GameObject arm;
    [SerializeField] protected GameObject hand;
    [SerializeField] protected Material armColor;

    public bool animating;

    protected IEnumerator coroutine;
    protected MeshRenderer handRenderer;

    private Transform armTransform;
    private Transform handTransform;

    protected virtual void Start()
    {
        armTransform = arm.transform;
        handTransform = hand.transform;
        handRenderer = hand.GetComponent<MeshRenderer>();
    }

    protected virtual void Update()
    {
        if (ClosestRelativeToArm().magnitude <= targetRange)
        {
            FloorManager.currentFloor.smudgeManager.SelectSmudge(CharacterMover.closestSmudge);
        }
        else
        {
            transform.rotation = Quaternion.identity;
            FloorManager.currentFloor.smudgeManager.DeselectSmudge();
        }
    }

    protected Vector3 ClosestRelativeToArm()
    {
        return CharacterMover.closestRelativePosition - transform.localPosition;
    }

    // Used to extend the arm to a specified distance
    protected void StretchArm(float length)
    {
        handTransform.localPosition = length * Vector3.forward;
        armTransform.localPosition = length / 2 * Vector3.forward;

        Vector3 localScale = armTransform.localScale;
        localScale = new Vector3(localScale.x, localScale.y, length);
        armTransform.localScale = localScale;
    }
}