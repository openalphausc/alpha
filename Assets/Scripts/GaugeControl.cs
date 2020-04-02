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

    public float decreaseSpeed;
    public float increaseSpeed;

    // Start is called before the first frame update
    void Start()
    {
      sprayController = sprayArmJoint.GetComponent<SprayController>();
      inputHandler = character.GetComponent<InputHandler>();

      bottom = transform.position.y - transform.localScale.y/2.0f;
      startPos = transform.position.y;
      startScale = transform.localScale.y;

      decreaseSpeed = 1f;
      increaseSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
      if(sprayController.animating && sprayController.transform.rotation.y != 0) {
        // adjust size of gauge front to make it seem like it's going down
        float targetPercent = 100*inputHandler.fluidRemaining/inputHandler.maxFluid;
        float currPercent = 100*transform.localScale.y/startScale;
        if(currPercent > targetPercent && currPercent > 0 && currPercent <= 100) {
          // decrease speed is proportional to how much distance is left
          float decrease = (currPercent - targetPercent)*0.002f*decreaseSpeed;
          transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - decrease, transform.localScale.z);
        }
        transform.position = new Vector3(transform.position.x, gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position.y - 14 + bottom + transform.localScale.y/2.0f, 0);
      }
      else if(inputHandler.refilling) {
        // adjust size of gauge front to make it seem like it's going down
        float targetPercent = 100;
        float currPercent = 100*transform.localScale.y/startScale;
        Debug.Log("currPercent is " + transform.localScale.y + " / " + startScale);
        if(currPercent < targetPercent - 8) {
          // increase speed is proportional to how much distance is left
          float increase = (targetPercent - currPercent)*0.002f*increaseSpeed;
          Debug.Log("increase is " + increase);
          transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + increase, transform.localScale.z);
        }
        else {
          inputHandler.refilling = false;
          inputHandler.fluidRemaining = inputHandler.maxFluid;
        }

        transform.position = new Vector3(transform.position.x, gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position.y - 14 + bottom + transform.localScale.y/2.0f, 0);
      }
    }
}
