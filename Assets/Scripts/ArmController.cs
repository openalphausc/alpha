using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmController : MonoBehaviour
{

    
    [SerializeField] protected float targetRange;
    [SerializeField] protected float maxArmLength;
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
        if (CharacterMover.closestRelativePosition.magnitude <= targetRange)
        {
            // transform.LookAt(closestPosition + this.transform.position);
            SmudgeManager.SelectSmudge(CharacterMover.closestSmudge);
        }
        else
        {
            transform.rotation = Quaternion.identity;
            SmudgeManager.DeselectSmudge();
        }
    }

    protected Vector3 ClosestRelativeToArm()
    {
        return CharacterMover.closestRelativePosition - transform.localPosition;
    }

    protected void StretchArm(float length)
    {
        handTransform.localPosition = length * Vector3.forward;
        armTransform.localPosition = length / 2 * Vector3.forward;

        Vector3 localScale = armTransform.localScale;
        localScale = new Vector3(localScale.x, localScale.y, length);
        armTransform.localScale = localScale;
    }
}