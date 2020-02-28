using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public float maxArmLength;
    public float targetRange;

    public GameObject arm;
    public GameObject hand;

    public Material sprayColorJ;
    public Material sprayColorK;
    public Material sprayColorL;
    public Material wiperColor;

    private MeshRenderer handRenderer;
    private Transform armTransform;
    private Transform handTransform;
    private IEnumerator coroutine;
    private Dictionary<Smudge.SmudgeType, Material> sprayColor = new Dictionary<Smudge.SmudgeType, Material>();
    private bool animating;

    void Start()
    {
        armTransform = arm.transform;
        handTransform = hand.transform;
        handRenderer = hand.GetComponent<MeshRenderer>();
        sprayColor[Smudge.SmudgeType.smudgeJ] = sprayColorJ;
        sprayColor[Smudge.SmudgeType.smudgeK] = sprayColorK;
        sprayColor[Smudge.SmudgeType.smudgeL] = sprayColorL;
    }

    void Update()
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

        if (CharacterMover.closestRelativePosition.magnitude <= maxArmLength)
        {
            StretchArm(CharacterMover.closestRelativePosition.magnitude);
        }
        else
        {
            StretchArm(maxArmLength);
        }
    }

    public void AnimateSpray(Smudge.SmudgeType spray)
    {
        if (animating)
        {
            StopCoroutine(coroutine);
        }
        handRenderer.material = sprayColor[spray];
        transform.LookAt(CharacterMover.closestRelativePosition + this.transform.position);
        //particles
        coroutine = FinishSpray();
        StartCoroutine(coroutine);
    }

    IEnumerator FinishSpray()
    {
        animating = true;
        yield return new WaitForSeconds(1);
        transform.rotation = Quaternion.identity;
        handRenderer.material = wiperColor;
        animating = false;
    }

    void AnimateWipe()
    {
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