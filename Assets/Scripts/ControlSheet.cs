using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSheet : MonoBehaviour
{
     
    public GameObject controlSheet;
    // Update is called once per frame

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H presssed");
            controlSheet.SetActive(!controlSheet.activeInHierarchy);
        }
    }
}
