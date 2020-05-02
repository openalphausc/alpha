using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(worldScreenWidth, worldScreenHeight, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
