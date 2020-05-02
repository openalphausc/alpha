using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{
    public GameObject dirtLayer;
    float percent;
    // Start is called before the first frame update
    void Start()
    {
        percent = PersistentManagerScript.Instance.levelProgress;
        float heightChange = dirtLayer.transform.GetComponent<RectTransform>().rect.height * percent;
        dirtLayer.transform.localScale = new Vector3(dirtLayer.transform.localScale.x, dirtLayer.transform.localScale.y * (1.0f - percent), dirtLayer.transform.localScale.z);
        dirtLayer.transform.localPosition = new Vector3(dirtLayer.transform.localPosition.x, dirtLayer.transform.localPosition.y - (heightChange / 2.0f), dirtLayer.transform.localPosition.z);
    }
}
