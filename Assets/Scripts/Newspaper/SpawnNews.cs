using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNews : MonoBehaviour
{
    float percent;
    public GameObject parent;
    public GameObject volcano;
    public GameObject squid;
    public GameObject sMonster;
    // Start is called before the first frame update
    void Start()
    {
        percent = PersistentManagerScript.Instance.levelProgress;
        GameObject news;
        int number = Random.Range(1, 3);
        if(number == 1)
        {
            news = Instantiate(volcano, parent.transform);
        }
        if (number == 2)
        {
            news = Instantiate(squid, parent.transform);
        }
        if (number == 3)
        {
            news = Instantiate(sMonster, parent.transform);
        }
    }
}
