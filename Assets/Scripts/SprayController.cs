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

    protected override void Update()
    {
        base.Update();
    }

    // begins the spray animation process
    public void AnimateSpray(Smudge.SmudgeType spray, bool showSprayParticles)
    {
        if (animating)
        { //animation cancel
            StopCoroutine(coroutine);
        }

        handRenderer.color = sprayColor[spray];

        // aim the spraying arm
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
        
        // spray particles
        if(showSprayParticles) {
          ParticleSystem.MainModule particlesMain = particles.main;
          particlesMain.startColor =
              handRenderer.color;
          particles.Play();
          //if there's enough fluid (shiwSprayParticles), then play the spray sound effect
          source.Play();
        }
        coroutine = FinishSpray();
        StartCoroutine(coroutine);
    }

    // after delay, retract arm
    private IEnumerator FinishSpray()
    {
        animating = true;
        yield return new WaitForSeconds(0.5f);
        transform.rotation = Quaternion.identity;
        animating = false;
    }
}
