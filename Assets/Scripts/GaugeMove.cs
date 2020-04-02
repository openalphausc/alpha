using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeMove : MonoBehaviour
{
    public GameObject sprayArmJoint;
    private SprayController sprayController;

    public GameObject character;
    private InputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
      sprayController = sprayArmJoint.GetComponent<SprayController>();
      inputHandler = character.GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
      if(inputHandler.refilling || (sprayController.animating && sprayController.transform.rotation.y != 0)) {
        // gauge shows up when player is spraying or refilling
        // next to the player on the opposite side they're spraying on
        float offset = 2f;
        if(sprayController.transform.rotation.y > 0) offset *= -1;
        float z = 5f;
        transform.position = new Vector3(sprayController.transform.position.x + offset, sprayController.transform.position.y, z);
      }
      else {
        // gauge disappears
        transform.position = new Vector3(-100f, -100f, 100f);
      }
    }
}
