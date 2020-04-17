using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendFeedback()
    {
        Application.OpenURL("mailto:openalpha@usc.edu?subject=Wishy%20Washy%20Feedback&body=Device:%20" + SystemInfo.deviceModel + "%0A" + "OS:%20" + SystemInfo.operatingSystem + "%0A%0A%0A%0A");
    }
}
