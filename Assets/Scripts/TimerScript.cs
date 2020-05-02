using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text uiText;

    private float timer;
    public List<float> trackSplits;
    public bool runTimer;

    void Start()
    {
        timer = 0;
        runTimer = true;
        trackSplits = new List<float>();
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
            addTime();
            PersistentManagerScript.Instance.floorSplits.Add((timer));
            timer = 0;
            runTimer = true;
        }
        
    }

    public void addTime()
    {
        
        trackSplits.Add(timer);
    }}
