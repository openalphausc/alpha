using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{

    public enum AnimationState
    {
        complete,
        spraying,
        wiping,
    }
    
    public float targetRange;
    public float wipeRange;
    public float maxArmLength;

    public GameObject arm;
    public GameObject hand;
    public ParticleSystem particles;

    public Material sprayColorJ;
    public Material sprayColorK;
    public Material sprayColorL;
    public Material wiperColor;
    public Material armColor;
    
    public AnimationState animationState = AnimationState.complete;

    private MeshRenderer handRenderer;
    private Transform armTransform;
    private Transform handTransform;
    private IEnumerator coroutine;
    private Dictionary<Smudge.SmudgeType, Material> sprayColor = new Dictionary<Smudge.SmudgeType, Material>();

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
    }

    public void AnimateSpray(Smudge.SmudgeType spray)
    {
        if (animationState != AnimationState.complete)
        {
            StopCoroutine(coroutine);
        }

        handRenderer.material = sprayColor[spray];

        transform.LookAt(CharacterMover.closestRelativePosition + this.transform.position);
        if (CharacterMover.closestRelativePosition.magnitude <= maxArmLength * 2)
        {
            StretchArm(CharacterMover.closestRelativePosition.magnitude / 2);
        }
        else
        {
            StretchArm(maxArmLength);
        }

        ParticleSystem.MainModule particlesMain = particles.main;
        particlesMain.startColor = handRenderer.material.color;
        particles.Play();
        coroutine = FinishSpray();
        StartCoroutine(coroutine);
    }

    IEnumerator FinishSpray()
    {
        animationState = AnimationState.spraying;
        yield return new WaitForSeconds(0.5f);
        transform.rotation = Quaternion.identity;
        handRenderer.material = armColor;
        animationState = AnimationState.complete;
    }

    public void AnimateWipe()
    {
        if (animationState != AnimationState.complete)
        {
            StopCoroutine(coroutine);
        }

        handRenderer.material = wiperColor;
        transform.LookAt(CharacterMover.closestRelativePosition + this.transform.position);
        if (CharacterMover.closestRelativePosition.magnitude <= maxArmLength)
        {
            StretchArm(CharacterMover.closestRelativePosition.magnitude);
        }
        else
        {
            StretchArm(maxArmLength);
        }

        if (CharacterMover.closestRelativePosition.magnitude <= wipeRange)
        {
            SmudgeManager.WipeSmudge();
        }
        coroutine = FinishWipe();
        StartCoroutine(coroutine);
    }

    IEnumerator FinishWipe()
    {
        animationState = AnimationState.wiping;
        yield return new WaitForSeconds(0.5f);
        transform.rotation = Quaternion.identity;
        handRenderer.material = armColor;
        animationState = AnimationState.complete;
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