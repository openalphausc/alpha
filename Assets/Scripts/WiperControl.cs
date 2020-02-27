using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiperControl : MonoBehaviour
{

    public float wiperSpeed;
    public float maxDistanceFromBody;
    public bool tooFarX;
    public bool tooFarY;

    private Vector3 mousePosition;
    public Vector3 targetPosition;

    private PlayerController playerScript;
    private Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        wiperSpeed = 0.07f;
        maxDistanceFromBody = 5f;

        playerScript = GameObject.Find("Body").GetComponent<PlayerController>();
        playerTransform = GameObject.Find("Body").transform;
    }

    // Update is called once per frame
    void Update()
    {
      mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
      targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
      float distanceX = Mathf.Abs(targetPosition.x - playerTransform.position.x);
      float distanceY = Mathf.Abs(targetPosition.y - playerTransform.position.y);
      tooFarX = distanceX > maxDistanceFromBody;
      tooFarY = distanceY > maxDistanceFromBody;

      // only move the wiper with the body if it's out of range (otherwise the mouse still has full control)
      float moveX = 0;
      float playerSpeed = playerScript.speed;
      if(Input.GetKey("d")) moveX += playerSpeed;
      else if(Input.GetKey("a")) moveX -= playerSpeed;
      transform.position = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);
      if(!tooFarX) {
        transform.position = new Vector3(Vector3.Lerp(transform.position, targetPosition, wiperSpeed).x, transform.position.y, transform.position.z);
      }
      if(!tooFarY) {
        transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, targetPosition, wiperSpeed).y, transform.position.z);
      }
    }
}
