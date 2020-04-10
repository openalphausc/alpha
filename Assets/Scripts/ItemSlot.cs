using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Image image;
    [SerializeField] Button button;
    
    void Start(){
        image.sprite = item.Icon;
        button.GetComponentInChildren<TextMeshProUGUI>().text = item.price.ToString();
    }
    
    public void Buy(){
        PersistentManagerScript.Instance.Buy(item);
    }

}