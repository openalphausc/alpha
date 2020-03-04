using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float movementSpeed;

    public float fallMultiplier = 2.5f;

    public float lowJumpMultiplier = 2.0f;

    public Rigidbody rb;

    public LayerMask platform;

    public float jumpForce = 7;

    private bool inAir;

    public BoxCollider col;

    private bool WPressed;

    void Start()
    {
        inAir = false;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        WPressed = false;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
        }
        if ((Input.GetKey(KeyCode.W) && !WPressed) && !inAir)
        {
            rb.velocity = Vector3.up * jumpForce;
            inAir = true;
            WPressed = true;
        }
        if (!Input.GetKey(KeyCode.W))
        {
            WPressed = false;
        }

        //Gravity effects for a more video game feeling jump
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }


    void OnCollisionEnter(Collision dataFromCollision)
    {

        string name = dataFromCollision.gameObject.name;
        Debug.Log("name: " + name);
        if (name == "Platform")
        {
            inAir = false;
        }
    }
}
