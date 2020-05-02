using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    [SerializeField] Image image;
    [SerializeField] Button button;
    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text flavorText;
    [SerializeField] TMP_Text description;
    [SerializeField] GameObject descriptionPanel;
    [SerializeField] GameObject PurchasedPanel;

    public AudioSource kaching;
    public AudioSource error;

    private void Start(){
        if(item != null)
        {
            image.sprite = item.icon;
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.price.ToString();
            name.text = item.name;
            //change this to item.getDescription() later.
            description.text = item.getItemDescription();
            flavorText.text = item.itemFlavorText;
        }
    }

    public void Refresh(){
        Start();
    }

    //Method call to trigger when pressing the button.
    //If successful, change the button text or something to look different
    //and update the image to show that the item has been bought
    public void Buy(){
        if (PersistentManagerScript.Instance.Buy(item, kaching, error))
        {
            //darken sprite
            PurchasedPanel.SetActive(true);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Purchased";
        }

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
