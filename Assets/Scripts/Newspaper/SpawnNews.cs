﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNews : MonoBehaviour
{
    float percent;
    // Start is called before the first frame update
    void Start()
    {
        percent = PersistentManagerScript.Instance.levelProgress;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}