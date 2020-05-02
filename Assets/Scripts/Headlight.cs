using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine;

public class Headlight : MonoBehaviour
{
    public float time;
    private Light2D L1;
    private float current;
    // Start is called before the first frame update
    void Start()
    {
        L1 = GetComponent<Light2D>();
        L1.intensity = 0;
        current = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        current += Time.deltaTime;
        if (current > time)
        {
            L1.intensity = 3;
        }
    }
}
