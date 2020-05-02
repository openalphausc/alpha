using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Superclass for WiperController and SprayController
public abstract class ArmController : MonoBehaviour
{
    [SerializeField] protected float maxArmLength; // maximum distance player can reach
    [SerializeField] protected GameObject arm;
    [SerializeField] protected GameObject hand;

    public bool animating;

    protected IEnumerator coroutine;
    protected SpriteRenderer handRenderer;

    private Transform armTransform;
    private Transform handTransform;

    protected virtual void Start()
    {
        armTransform = arm.transform;
        handTransform = hand.transform;
        handRenderer = hand.GetComponent<SpriteRenderer>();
        
        //apply reach increase from items
        maxArmLength *= (float) (1 + PersistentManagerScript.Instance.InvRangeIncrease()/100);
    }

    protected Vector3 ClosestRelativeToArm()
    {
        return CharacterMover.closestRelativePosition - transform.localPosition;
    }

    // Used to extend the arm to a specified distance
    protected void StretchArm(float length)
    {
        handTransform.localPosition = (length - 0.2f) * Vector3.forward;
        armTransform.localPosition = (length / 2 - 0.2f) * Vector3.forward;
        handTransform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
        armTransform.localRotation = Quaternion.Euler(new Vector3(0f, -90f, 90f));

        Vector3 localScale = armTransform.localScale;
        localScale = new Vector3(2, length);
        armTransform.localScale = localScale;
    }

    protected void RestArm()
    {
        transform.rotation = Quaternion.Euler(90f, 90f, 0f);
        StretchArm(2);
    }
}