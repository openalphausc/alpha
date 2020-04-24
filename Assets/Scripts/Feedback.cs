using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    public void SendFeedback()
    {
        Application.OpenURL("mailto:openalpha@usc.edu?subject=Wishy%20Washy%20Feedback&body=Device:%20" + SystemInfo.deviceModel + "%0A" + "OS:%20" + SystemInfo.operatingSystem + "%0A%0A%0A%0A");
    }
}