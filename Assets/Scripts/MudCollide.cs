using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudCollide : MonoBehaviour
{
    void OnCollisionStay(Collision other)
    {
        if (other.collider.name == "Wiper")
        {
            Destroy(gameObject);
        }
    }
}
