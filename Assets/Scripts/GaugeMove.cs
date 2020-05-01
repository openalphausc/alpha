using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeMove : MonoBehaviour
{
    public GameObject sprayArmJoint;
    private SprayController sprayController;

    public GameObject character;
    private InputHandler inputHandler;

    public Camera cam;

    public bool decreasing = false;

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
      // x and y are between 0 and cam.pixelWidth and cam.pixelHeight
      float x = 0.01f * cam.pixelWidth;
      if(gameObject.name == "FluidGaugeJ") x *= 84f; // percents across the screen - CHANGE THESE
      if(gameObject.name == "FluidGaugeK") x *= 87f;
      else if(gameObject.name == "FluidGaugeL") x *= 90f;
      float y = 0.01f * cam.pixelHeight;
      y *= 10f; // percent across the screen - CHANGE THIS
      float z = -9f;

      Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(x, y, cam.nearClipPlane));
      transform.position = new Vector3(worldPoint.x, worldPoint.y, z);
    }
}
