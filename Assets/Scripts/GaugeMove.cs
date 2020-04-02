using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeMove : MonoBehaviour
{
    public GameObject sprayArmJoint;
    private SprayController sprayController;

    public GameObject character;
    private InputHandler inputHandler;

    public int fluidIndex;

    // Start is called before the first frame update
    void Start()
    {
      sprayController = sprayArmJoint.GetComponent<SprayController>();
      inputHandler = character.GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
      if(inputHandler.refilling[fluidIndex] || (sprayController.animating && sprayController.transform.rotation.y != 0)) {
        // gauge shows up when player is spraying or refilling
        // at the bottom-left of the screen
        float x = -12f;
        if(gameObject.name == "FluidGaugeK") x += 1f;
        else if(gameObject.name == "FluidGaugeL") x += 2f;
        float y = -6f;
        float z = 5f;
        transform.position = new Vector3(x, sprayController.transform.position.y + y, z);
      }
      else {
        // gauge disappears
        transform.position = new Vector3(-100f, -100f, 100f);
      }
    }
}
