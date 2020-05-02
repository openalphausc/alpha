using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubwayScript : MonoBehaviour
{
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;   
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 10.0f)
        {
            SceneManager.LoadScene("NewspaperScene");
        }
    }
}
