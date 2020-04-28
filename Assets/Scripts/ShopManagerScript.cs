using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public Text MoneyTxt;
    //Item Slot Prefabs
    public GameObject SmallItemSlot;
    public GameObject MediumItemSlot;
    public GameObject LargeItemSlot;
    // Start is called before the first frame update
    //Set the text managed by this manager to the player's money total
    void Start()
    {
        MoneyTxt.text = PersistentManagerScript.Instance.money.ToString();
    }

    //Template code
    public void AttemptPurchase()
    {

    }

    //Template code
    public void GoToSecondScene(){

    }

    //used for debugging & testing
    public void IncreaseMoney(){
        PersistentManagerScript.Instance.money++;
    }
}
