using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine;

public class SunLight : MonoBehaviour
{
    public float dayLength;
    public float scale;
    private Light2D L1;
    private float current;
    // Start is called before the first frame update
    void Start()
    {
        L1 = GetComponent<Light2D>();
        L1.intensity = 3;
        current = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        current += Time.deltaTime;
        if (L1.intensity > 0.0f && current > dayLength)
        {
            L1.intensity -= Time.deltaTime * scale;
        }
    }
}
