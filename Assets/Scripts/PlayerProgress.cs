using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{
    float percent;
    // Start is called before the first frame update
    void Start()
    {
        percent = PersistentManagerScript.Instance.levelProgress;
        float heightChange = transform.GetComponent<RectTransform>().rect.height * percent;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * (1.0f - percent), transform.localScale.z);
    }
}
