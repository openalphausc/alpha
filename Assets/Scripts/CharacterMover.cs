using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float movementSpeed;

    public Rigidbody rb;

    public LayerMask platform;

    public float jumpForce = 7;

    private bool inAir;

    public BoxCollider col;


    void Start()
    {
        inAir = false;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
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
        //utilizing the addforce function gives the fall a more realistic feel
        if (Input.GetKey(KeyCode.W) && !inAir)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            inAir = true;
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
