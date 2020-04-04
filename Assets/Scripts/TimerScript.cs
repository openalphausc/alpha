using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private Text uiText;

    private float timer;
    public readonly float[] trackSplits;
    public bool runTimer;
    private int stageCount;

    void Start()
    {
        timer = 0;
        runTimer = true;
        stageCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (runTimer)
        {
            timer += Time.deltaTime;
            uiText.text = timer.ToString("F");
        }

        else
        {
            trackSplits[stageCount] = timer;
            stageCount++;
            timer = 0;
        }
        
    }
}
