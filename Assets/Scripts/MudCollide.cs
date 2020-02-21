using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudCollide : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Wiper" && Input.GetKey("space"))
        {
            Destroy(gameObject);
        }
    }
}
