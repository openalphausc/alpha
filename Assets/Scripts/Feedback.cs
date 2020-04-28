using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    public void SendFeedback()
    {
        Application.OpenURL("mailto:oateam@usc.edu?subject=Wishy%20Washy%20Feedback%20Version%20" + Application.version + "&body=Device:%20" + SystemInfo.deviceModel + "%0A" + "OS:%20" + SystemInfo.operatingSystem + "%0A%0A%0A%0A");
    }
    public void FeedbackForm()
    {
        Application.OpenURL("https://forms.gle/RHuaBSnwrebVT8cg8");
    }
}