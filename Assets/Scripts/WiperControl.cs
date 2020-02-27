using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiperControl : MonoBehaviour
{

    public float wiperSpeed;
    public float maxDistanceFromBody;
    public bool tooFarX;
    public bool tooFarY;

    public float sprayCooldownMax;
    public float sprayCooldown;
    public GameObject fluidPrefab;

    private Vector3 mousePosition;
    public Vector3 targetPosition;

    private PlayerController playerScript;
    private Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        wiperSpeed = 0.4f;
        maxDistanceFromBody = 5f;

        sprayCooldownMax = 10f;
        sprayCooldown = sprayCooldownMax;

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

      // move the wiper with the mouse, and move it with the body if it's being dragged along
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

      // spray cleaning fluid
      if(sprayCooldown < sprayCooldownMax) {
        sprayCooldown--;
        if(sprayCooldown <= 0) sprayCooldown = sprayCooldownMax;
      }
      else {
        if(Input.GetKey(KeyCode.Space)) {
          // spawn fluid particle
          Instantiate(fluidPrefab, transform.position, Quaternion.identity);
          sprayCooldown--;
        }
      }
    }
}
