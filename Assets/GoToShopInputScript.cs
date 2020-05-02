﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToShopInputScript : MonoBehaviour
{
    
    private float HoldADTime;
    public RectTransform HoldADFill;
    public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            HoldADTime += Time.deltaTime;
            HoldADFill.transform.localScale = new Vector3(HoldADTime,1,1);
            print("been holding for " + (int)HoldADTime);
            if (HoldADTime > 1f)
            {
                ChangeSceneScript.ChangeScene(nextScene);
            }
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            HoldADTime = 0;
            HoldADFill.transform.localScale = new Vector3(HoldADTime,1,1);
            print("been holding for " + (int)HoldADTime);
        }
    }
}
