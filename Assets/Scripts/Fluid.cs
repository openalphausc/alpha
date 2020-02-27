using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      float gravity = 0.01f;
      transform.position = new Vector3(transform.position.x, transform.position.y - gravity, transform.position.z);

    }
}
