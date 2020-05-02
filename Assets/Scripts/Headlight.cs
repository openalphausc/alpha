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
        //to prevent checking more than once
        bool on = false;
        if (current > time && !on)
        {
            //Don't want to check this more than once, since it's an expensive function
            if(PersistentManagerScript.Instance.InvHeadlamp())
                L1.intensity = 3;
            on = true;
        }
    }
}
