using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private Text uiText;

    private float timer;

    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (FloorManager.currentFloor.smudgeManager.allSmudges.Count > 0)
        {
            timer += Time.deltaTime;
            uiText.text = timer.ToString("F");
        }

        //uiText.text = "10.00";
        
    }
}
