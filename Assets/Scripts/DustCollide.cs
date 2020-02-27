using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCollide : MonoBehaviour
{
    void OnCollisionStay(Collision other)
    {
        if (other.collider.name == "Wiper")
        {
            Destroy(gameObject);
        }
    }
}
