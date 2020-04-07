using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeControl : MonoBehaviour
{
    public GameObject sprayArmJoint;
    private SprayController sprayController;

    public GameObject character;
    private InputHandler inputHandler;

    public float startPos;
    public float startScale;
    private float bottom;

    private GaugeMove gaugeMove;
    public float decreaseSpeed;

    public float increaseSpeed;

    public int fluidIndex;

    private CharacterMover characterMover;

    // Start is called before the first frame update
    void Start()
    {
      sprayController = sprayArmJoint.GetComponent<SprayController>();
      inputHandler = character.GetComponent<InputHandler>();

      bottom = transform.position.y - transform.localScale.y/2.0f;
      startPos = transform.position.y;
      startScale = transform.localScale.y;

      gaugeMove = gameObject.transform.parent.gameObject.GetComponent<GaugeMove>();

      decreaseSpeed = 1f;
      increaseSpeed = 0.005f;

      characterMover = character.GetComponent<CharacterMover>();
    }

    // Update is called once per frame
    void Update()
    {
      if(gaugeMove.decreasing) {
        // adjust size of gauge front to make it seem like it's going down
        float targetPercent = 100*inputHandler.fluidRemaining[fluidIndex]/inputHandler.maxFluid;
        float currPercent = 100*transform.localScale.y/startScale;
        if(currPercent > targetPercent && currPercent > 0) {
          // decrease speed is proportional to how much distance is left
          float decrease = (currPercent - targetPercent)*0.002f*decreaseSpeed;
          transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - decrease, 1f);
        }
        if(!sprayController.animating) {
          gaugeMove.decreasing = false;
        }
        transform.position = new Vector3(transform.position.x, gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position.y - 17 + bottom + transform.localScale.y/2.0f, 1f);
      }
      else if(inputHandler.refilling[fluidIndex]) {
        // adjust size of gauge front to make it seem like it's going down
        float currPercent = 100*transform.localScale.y/startScale;
        inputHandler.fluidRemaining[fluidIndex] = Mathf.Round(inputHandler.maxFluid*currPercent/100);
        if(currPercent < 99) {
          // increase speed is proportional to how much distance is left
          float power = 2f;
          float offset = 1f;
          float x = currPercent/100;
          float increase = increaseSpeed * Mathf.Pow(offset + x, power) / (power - 1);
          float min = 0.004f;
          if(increase < min) increase = min;

          transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + increase, 1f);
        }
        else {
          transform.localScale = new Vector3(transform.localScale.x, startScale, transform.localScale.z);
          inputHandler.fluidRemaining[fluidIndex] = inputHandler.maxFluid;
          inputHandler.refilling[fluidIndex] = false;
          // start animation of cleaning up
          characterMover.speedState = 1;
          characterMover.timeCleaningUp = (int)(2f * 60);
        }

        transform.position = new Vector3(transform.position.x, gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position.y - 17 + bottom + transform.localScale.y/2.0f, 1f);
      }
      else if(inputHandler.fluidRemaining[fluidIndex] == 0) inputHandler.refilling[fluidIndex] = true;
    }
}
