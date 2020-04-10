using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public Text MoneyTxt;

    // Start is called before the first frame update
    void Start()
    {
        MoneyTxt.text = PersistentManagerScript.Instance.money.ToString();
    }

    
    public void AttemptPurchase()
    {

    }   

    public void GoToSecondScene(){
        
    }
    public void increaseMoney(){
        PersistentManagerScript.Instance.money++;
    }
}
