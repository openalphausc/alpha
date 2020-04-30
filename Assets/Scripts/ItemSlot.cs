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

    public AudioSource kaching;
    public AudioSource error;

    private void Start(){
        image.sprite = item.icon;
        button.GetComponentInChildren<TextMeshProUGUI>().text = item.price.ToString();
    }

    //Method call to trigger when pressing the button.
    //If successful, change the button text or something to look different
    //and update the image to show that the item has been bought
    public void Buy(){
        //for now, just run the buy function.
        print("uhhh");
        PersistentManagerScript.Instance.Buy(item, kaching, error);
    }

}
