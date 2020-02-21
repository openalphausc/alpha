using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.07f;
    }

    void OnCollisionEnter2D(Collision2D dataFromCollision)
    {

        string name = dataFromCollision.gameObject.name;

    }

    // Update is called once per frame
    void Update()
    {
      // movement left/right
      float moveX = 0;
      if(Input.GetKey("d")) moveX = speed;
      else if(Input.GetKey("a")) moveX = -speed;
      transform.position = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);

      // input to restart the scene/quit
      // if(Input.GetKey("r")) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      if(Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
    }
}
