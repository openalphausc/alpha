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
      if(inputHandler.refilling[fluidIndex] || (decreasing && sprayController.animating)) {
        // gauge shows up when player is spraying or refilling
        // x and y are between 0 and cam.pixelWidth and cam.pixelHeight
        float x = 0.01f * cam.pixelWidth;
        if(gameObject.name == "FluidGaugeJ") x *= 9f; // percents across the screen
        if(gameObject.name == "FluidGaugeK") x *= 12f;
        else if(gameObject.name == "FluidGaugeL") x *= 15f;
        float y = 10f * 0.01f * cam.pixelHeight;
        float z = 5f;

        Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(x, y, cam.nearClipPlane));
        transform.position = new Vector3(worldPoint.x, worldPoint.y, z);
      }
      else {
        // gauge disappears
        transform.localPosition = new Vector3(-100f, -100f, 100f);
      }
    }
}
