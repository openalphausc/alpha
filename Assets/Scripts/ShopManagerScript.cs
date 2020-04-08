using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        SceneManager.LoadScene("first");
    }   

    public void GoToSecondScene(){
        SceneManager.LoadScene("second");
    }
    public void increaseMoney(){
        PersistentManagerScript.Instance.money++;
    }
}
