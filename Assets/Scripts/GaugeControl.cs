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

    // Start is called before the first frame update
    void Start()
    {
      sprayController = sprayArmJoint.GetComponent<SprayController>();
      inputHandler = character.GetComponent<InputHandler>();

      // mfX = transform.position.x + transform.localScale.x/2.0f;
      bottom = transform.position.y - transform.localScale.y/2.0f;
      startPos = transform.position.y;
      startScale = transform.localScale.y;

      decreaseSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
      if(sprayController.animating && sprayController.transform.rotation.y != 0) {
        // gauge shows up when player is spraying
        float offset = 2f;
        if(sprayController.transform.rotation.y > 0) offset *= -1;
        float z = 5f;
        if(gameObject.name == "GaugeBack") z = 5f;
        else if(gameObject.name == "GaugeFront") z = 5.1f;
        transform.position = new Vector3(sprayController.transform.position.x + offset, sprayController.transform.position.y, z);
        if(gameObject.name == "GaugeFront") {
          // adjust size of gauge front to make it seem like it's going down
          float targetPercent = 100*inputHandler.fluidRemaining/inputHandler.maxFluid;
          float currPercent = 100*transform.localScale.y/startScale;
          Debug.Log("target: " + targetPercent + "  curr: " + currPercent);
          if(currPercent > targetPercent && currPercent > 0 && currPercent <= 100) {
            // decrease speed is proportional to how much distance is left
            float decrease = (currPercent - targetPercent)*0.002f*decreaseSpeed;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - decrease, transform.localScale.z);
          }
          transform.position = new Vector3(transform.position.x, bottom + transform.localScale.y/2.0f, 0);
        }
      }
      else {
        // gauge disappears when not spraying
        transform.position = new Vector3(-100f, -100f, 100f);
      }
    }
}
