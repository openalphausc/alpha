using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayController : ArmController
{
    public Material sprayColorJ;
    public Material sprayColorK;
    public Material sprayColorL;
    public ParticleSystem particles;
    
    private Dictionary<Smudge.SmudgeType, Material> sprayColor = new Dictionary<Smudge.SmudgeType, Material>();
    
    protected override void Start()
    {
        base.Start();
        
        sprayColor[Smudge.SmudgeType.smudgeJ] = sprayColorJ;
        sprayColor[Smudge.SmudgeType.smudgeK] = sprayColorK;
        sprayColor[Smudge.SmudgeType.smudgeL] = sprayColorL;
    }

    protected override void Update()
    {
        base.Update();
    }
    
    public void AnimateSpray(Smudge.SmudgeType spray)
    {
        if (animating)
        {
            StopCoroutine(coroutine);
        }

        handRenderer.material = sprayColor[spray];

        Vector3 closest = ClosestRelativeToArm();
        transform.LookAt(closest + transform.position);
        if (closest.magnitude <= maxArmLength * 2)
        {
            StretchArm(closest.magnitude / 2);
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

    private IEnumerator FinishSpray()
    {
        animating = true;
        yield return new WaitForSeconds(0.5f);
        transform.rotation = Quaternion.identity;
        handRenderer.material = armColor;
        animating = false;
    }
}
