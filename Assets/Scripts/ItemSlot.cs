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
    [SerializeField] TMP_Text name; 
    [SerializeField] TMP_Text description;
    [SerializeField] GameObject descriptionPanel;

    private void Start(){
        image.sprite = item.icon;
        button.GetComponentInChildren<TextMeshProUGUI>().text = item.price.ToString();
        name.text = item.name;
        //change this to item.getDescription() later.
        description.text = item.itemDescription;
    }
    
    //Method call to trigger when pressing the button.
    //If successful, change the button text or something to look different
    //and update the image to show that the item has been bought
    public void Buy(){
        //for now, just run the buy function.
        print("uhhh");
        PersistentManagerScript.Instance.Buy(item);
    }

    public void ShowDescriptionPanel()
    {
        descriptionPanel.SetActive(true);
    }
    public void HideDescriptionPanel()
    {
        descriptionPanel.SetActive(false);
    }

}