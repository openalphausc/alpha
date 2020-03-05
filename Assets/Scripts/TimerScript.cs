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
        if (timer<= 10.0f)
        {
            timer += Time.deltaTime;
            uiText.text = timer.ToString("F");
        }
        
    }
}
