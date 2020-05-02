using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubwayScript : MonoBehaviour
{
    float time;
    public GameObject volcano;
    public GameObject squid;
    public GameObject spag;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        int number = Random.Range(1, 4);
        if(number == 1)
        {
            Instantiate(volcano);
        }
        if (number == 2)
        {
            Instantiate(squid);
        }
        if (number == 3)
        {
            Instantiate(spag);
        }
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
