using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class MoneyScript : MonoBehaviour
{
    [SerializeField] private TMP_Text uiText;

    private int stageCount;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        uiText.text = "$" + PersistentManagerScript.Instance.money.ToString();
        
    }
}
