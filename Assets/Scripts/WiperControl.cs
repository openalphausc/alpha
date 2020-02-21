using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiperControl : MonoBehaviour
{

    public float wiperSpeed;
    public float maxDistanceFromBody;
    public bool tooFar;

    private Vector3 mousePosition;
    private Vector3 targetPosition;

    private PlayerController playerScript;
    private Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        wiperSpeed = 0.07f;
        maxDistanceFromBody = 6f;

        playerScript = GameObject.Find("Body").GetComponent<PlayerController>();
        playerTransform = GameObject.Find("Body").transform;
    }

    // Update is called once per frame
    void Update()
    {
      mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
      targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
      float distanceFromBody = Mathf.Abs(Vector3.Distance(targetPosition, playerTransform.position));
      tooFar = distanceFromBody > maxDistanceFromBody;
      bool mouseControl = Input.GetMouseButton(0);

      // only move the wiper with the body if it's out of range (otherwise the mouse still has full control)
      if(tooFar || !mouseControl) {
        float moveX = 0;
        float playerSpeed = playerScript.speed;
        if(Input.GetKey("d")) moveX += playerSpeed;
        else if(Input.GetKey("a")) moveX -= playerSpeed;
        transform.position = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);
      }
      else if(mouseControl) {
        transform.position = Vector3.Lerp(transform.position, targetPosition, wiperSpeed);
      }
    }
}
