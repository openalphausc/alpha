using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls the spraying arm
public class SprayController : ArmController
{
    public ParticleSystem particles;

    private Dictionary<Smudge.SmudgeType, Color> sprayColor = new Dictionary<Smudge.SmudgeType, Color>();

    private AudioSource source;

    protected override void Start()
    {
        base.Start();

        sprayColor[Smudge.SmudgeType.SmudgeJ] = Color.red;
        sprayColor[Smudge.SmudgeType.SmudgeK] = Color.yellow;
        sprayColor[Smudge.SmudgeType.SmudgeL] = Color.green;

        //grab audio source for use in animateSpray
        source = GetComponent<AudioSource>();
    }

    // begins the spray animation process
    public bool AnimateSpray(Smudge.SmudgeType spray, bool showSprayParticles)
    {
        if (!CharacterMover.targeting)
        {
            return false;
        }
        
        if (animating)
        {
            //animation cancel
            StopCoroutine(coroutine);
        }

        handRenderer.color = sprayColor[spray];

        // aim the spraying arm
        Vector3 closest = ClosestRelativeToArm();

        StretchArm(closest.magnitude / 2);
        transform.LookAt(closest + transform.position);

        // spray particles
        if (showSprayParticles)
        {
            ParticleSystem.MainModule particlesMain = particles.main;
            particlesMain.startColor =
                handRenderer.color;
            particles.Play();
            //if there's enough fluid (showSprayParticles), then play the spray sound effect
            source.Play();
        }

        coroutine = FinishSpray();
        StartCoroutine(coroutine);
        return true;
    }

    // after delay, retract arm
    private IEnumerator FinishSpray()
    {
        animating = true;
        yield return new WaitForSeconds(0.5f);
        RestArm();
        animating = false;
    }
}