using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls the spraying arm
public class SprayController : ArmController
{
    public ParticleSystem particles;
    private Sprite sprayJ;
    private Sprite sprayK;
    private Sprite sprayL;

    private Dictionary<Smudge.SmudgeType, Sprite> sprayBottle = new Dictionary<Smudge.SmudgeType, Sprite>();
    private Dictionary<Smudge.SmudgeType, Color> sprayColor = new Dictionary<Smudge.SmudgeType, Color>();

    public AudioSource spray1;
    public AudioSource spray2;
    public AudioSource spray3;
    public AudioSource spraycreamy;
    public AudioSource spraythick;
    public AudioSource spraysquirt;

    protected override void Start()
    {
        base.Start();

        sprayBottle[Smudge.SmudgeType.SmudgeJ] = sprayJ;
        sprayBottle[Smudge.SmudgeType.SmudgeK] = sprayK;
        sprayBottle[Smudge.SmudgeType.SmudgeL] = sprayL;

        sprayColor[Smudge.SmudgeType.SmudgeJ] = Color.red;
        sprayColor[Smudge.SmudgeType.SmudgeK] = Color.yellow;
        sprayColor[Smudge.SmudgeType.SmudgeL] = Color.green;
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

        handRenderer.sprite = sprayBottle[spray];

        // aim the spraying arm
        Vector3 closest = ClosestRelativeToArm();

        StretchArm(closest.magnitude / 2);
        transform.LookAt(closest + transform.position);

        // spray particles
        if (showSprayParticles)
        {
            ParticleSystem.MainModule particlesMain = particles.main;
            particlesMain.startColor = sprayColor[spray];
            particles.Play();
            //if there's enough fluid (showSprayParticles), then play the spray sound effect
            if(spray == Smudge.SmudgeType.SmudgeJ || spray == Smudge.SmudgeType.SmudgeK) {
              // play normal sfx
              int choice = Random.Range(2, 4); // to allow spray1, change this to Random.Range(1, 4)
              if(choice == 1) spray1.Play(); // TODO temporarily disabled until spray1 is cut shorter
              else if(choice == 2) spray2.Play();
              else if(choice == 3) spray3.Play();
            }
            else if(spray == Smudge.SmudgeType.SmudgeL) {
              // play heavy sfx
              int choice = Random.Range(1, 4);
              if(choice == 1) spraycreamy.Play();
              else if(choice == 2) spraythick.Play();
              else if(choice == 3) spraysquirt.Play();
            }
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
