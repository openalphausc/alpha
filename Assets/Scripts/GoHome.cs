using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHome : MonoBehaviour
{
    public float time;
    public GameObject parent;
    public GameObject myPrefab;
    private float current;
    private bool once;
    // Start is called before the first frame update
    void Start()
    {
        current = 0.0f;
        once = false;
    }

    // Update is called once per frame
    void Update()
    {
        current += Time.deltaTime;
        if (current > time && once == false)
        {
            GameObject button = Instantiate(myPrefab) as GameObject;
            button.transform.position = new Vector3(parent.transform.position.x - 14.0f, parent.transform.position.y, 0.0f);
            once = true;
        }
    }
}
