using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    //Item Slot Prefabs
    public ItemSlot SmallItemSlot;
    public ItemSlot MediumItemSlot;
    public ItemSlot LargeItemSlot;

    public List<Item> smallItemsList;
    public List<Item> mediumItemsList;
    public List<Item> largeItemsList;

    private float HoldADTime;
    private float HoldSpaceTime;

    public string nextScene;
    // Start is called before the first frame update
    //Set the text managed by this manager to the player's money total
    void Start()
    {
        //RandomizeItems();
    }
    //make sure it happens before ItemSlot's start() runs
    void Awake()
    {
        RandomizeItems();
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            SmallItemSlot.ShowDescriptionPanel();
            MediumItemSlot.HideDescriptionPanel();
            LargeItemSlot.HideDescriptionPanel();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SmallItemSlot.HideDescriptionPanel();
            MediumItemSlot.ShowDescriptionPanel();
            LargeItemSlot.HideDescriptionPanel();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SmallItemSlot.HideDescriptionPanel();
            MediumItemSlot.HideDescriptionPanel();
            LargeItemSlot.ShowDescriptionPanel();
        }
        
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D))
        {
            HoldADTime += Time.deltaTime;
            print(HoldADTime);
            if (HoldADTime > 1f)
            {
                ChangeSceneScript.ChangeScene(nextScene);
            }
        }
        else
        {
            HoldADTime = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            SmallItemSlot.HideDescriptionPanel();
            MediumItemSlot.HideDescriptionPanel();
            LargeItemSlot.HideDescriptionPanel();
        }
        
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

    public void RandomizeItems()
    {
        //be sure to cull out items the player has already bought!
        foreach (var item in PersistentManagerScript.Instance.inventory)
        {
            smallItemsList.Remove(item);
            mediumItemsList.Remove(item);
            largeItemsList.Remove(item);
        }
        //randomize small item slot
        var index = Random.Range(0, smallItemsList.Count());
        if(smallItemsList.Any())
        SmallItemSlot.item = smallItemsList[index];
        
        //randomize Medium item slot
        index = Random.Range(0, mediumItemsList.Count());
        if(mediumItemsList.Any())
        MediumItemSlot.item = mediumItemsList[index];
        
        //randomize Large item slot
        index = Random.Range(0, largeItemsList.Count());
        if(largeItemsList.Any())
        LargeItemSlot.item = largeItemsList[index];
        
        SmallItemSlot.Refresh();
        MediumItemSlot.Refresh();
        LargeItemSlot.Refresh();
    }
    
}
