using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeControl : MonoBehaviour
{
    public GameObject sprayArmJoint;
    private SprayController sprayController;

    public GameObject character;
    private InputHandler inputHandler;

    public float startScale;
    private float bottom;

    private GaugeMove gaugeMove;
    public float decreaseSpeed;

    public float increaseSpeed;

    public int fluidIndex;

    private CharacterMover characterMover;

    private bool overflowing = false;
    private float overflowTime = -1f;
    private SpriteRenderer sprite;

    public AudioSource refillsound;
    public AudioSource overflowsound;

    // Start is called before the first frame update
    void Start()
    {
      sprayController = sprayArmJoint.GetComponent<SprayController>();
      inputHandler = character.GetComponent<InputHandler>();

      startScale = transform.localScale.y;
      bottom = -startScale/2;

      decreaseSpeed = 0.2f;
      increaseSpeed = 0.4f;

      gaugeMove = gameObject.transform.parent.gameObject.GetComponent<GaugeMove>();

      characterMover = character.GetComponent<CharacterMover>();

      sprite = GetComponent<SpriteRenderer>();
      print(sprite);
    }

    // Update is called once per frame
    void Update()
    {
      Color newColor;
      float dull = 150f/255;
      // flash when overflowing
      if(overflowing) {
        print("overflowing");
        overflowTime -= Time.deltaTime;
        if(characterMover.timeCleaningUp % 0.2f > 0.2f/2) newColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        else newColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, dull);
        if(characterMover.speedState == 0 || overflowTime <= 0) overflowing = false;
      }
      // bright when gauge increasing or decreasing
      else if(inputHandler.refilling[fluidIndex] || gaugeMove.decreasing) newColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
      // dull when gauge static
      else newColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, dull);
      sprite.color = newColor;

      transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.1f);
      if(gaugeMove.decreasing) {
        // adjust size of gauge front to make it seem like it's going down
        float targetPercent = 100*inputHandler.fluidRemaining[fluidIndex]/inputHandler.maxFluid;
        float currPercent = 100*transform.localScale.y/startScale;
        if(currPercent > targetPercent) {
          // decrease speed is proportional to how much distance is left
          float decrease = Time.deltaTime*(currPercent - targetPercent)*decreaseSpeed;
          transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - decrease, 1f);
        }
        else transform.localScale = new Vector3(transform.localScale.x, startScale*targetPercent/100, 1f);
        if(!sprayController.animating) {
          gaugeMove.decreasing = false;
        }
        if(currPercent < 0) transform.localScale = new Vector3(transform.localScale.x, 0.1f, 1f);
        transform.localPosition = new Vector3(0, bottom + transform.localScale.y/2f, -0.1f);
      }
      else if(inputHandler.refilling[fluidIndex]) {
        // adjust size of gauge front to make it seem like it's going down  
        float currPercent = 100*transform.localScale.y/startScale;
        inputHandler.fluidRemaining[fluidIndex] = Mathf.Round(inputHandler.maxFluid*currPercent/100);
        refillsound.volume = 1f - currPercent/100f;
        if(currPercent < 99) {
          // increase speed is proportional to how much distance is left
          float power = 2f;
          float offset = 1f;
          float x = currPercent/100;
          
          //EDIT THIS TO MULTIPLY ALSO BY THE PLAYER'S INCREASED REFILL SPEED BY PERCENTAGE
          float baseIncrease = Time.deltaTime * increaseSpeed * Mathf.Pow(offset + x, power) / (power - 1);
          float increase = baseIncrease * (float) (1 + PersistentManagerScript.Instance.InvRefillSpeedBonus()/100.0);
          float min = Time.deltaTime * 0.004f;
          if(increase < min) increase = min;

          transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + increase, -0.1f);
        }
        else {
          transform.localScale = new Vector3(transform.localScale.x, startScale, transform.localScale.z);
          inputHandler.fluidRemaining[fluidIndex] = inputHandler.maxFluid;
          inputHandler.refilling[fluidIndex] = false;
          // start animation of cleaning up
          characterMover.speedState = 1;
          //EDIT THIS TO MULTIPLY / REDUCE BY THE PLAYER'S PENALTY REDUCTION PERCENTAGE
          characterMover.timeCleaningUp = overflowTime = 2f * (float) (1 - (PersistentManagerScript.Instance.InvTimePenaltyReduction()/100));
          overflowing = true;
          overflowsound.Play();
        }

        transform.localPosition = new Vector3(0, bottom + transform.localScale.y/2f, -0.1f);
      }
      else if(inputHandler.fluidRemaining[fluidIndex] == 0) {
        inputHandler.refilling[fluidIndex] = true;
        refillsound.Play();
      }
    }
}
