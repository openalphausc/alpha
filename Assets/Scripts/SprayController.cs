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

    private AudioSource source;

    protected override void Start()
    {
        base.Start();

        sprayColor[Smudge.SmudgeType.smudgeJ] = sprayColorJ;
        sprayColor[Smudge.SmudgeType.smudgeK] = sprayColorK;
        sprayColor[Smudge.SmudgeType.smudgeL] = sprayColorL;
        //grab audio source for use in animateSpray
        source = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void AnimateSpray(Smudge.SmudgeType spray, bool showSprayParticles)
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
        if(showSprayParticles) {
          ParticleSystem.MainModule particlesMain = particles.main;
          particlesMain.startColor = handRenderer.material.color;
          particles.Play();
          //if there's enough fluid (shiwSprayParticles), then play the spray sound effect
          source.Play();
        }
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
