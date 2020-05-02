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
    public RectTransform HoldADFill;
    private float HoldSpaceTime;
    public RectTransform HoldSpaceFill;
    private int choice = (int)Choices.None;
    public string nextScene;

    public AudioSource error;

    enum Choices
    {
        None = -1,
        Small = 0,
        Medium,
        Large
    }

    // Start is called before the first frame update
    //Set the text managed by this manager to the player's money total
    void Start()
    {
        //RandomizeItems();
        HoldSpaceFill.transform.localScale = new Vector3(HoldSpaceTime,1,1);
        HoldADFill.transform.localScale = new Vector3(HoldADTime,1,1);
        PersistentManagerScript.Instance.levelIndex++;
    }
    //make sure it happens before ItemSlot's start() runs
    void Awake()
    {
        RandomizeItems();
    }

    //Handles input from player
    void Update()
    {
        //select J
        if (Input.GetKeyDown(KeyCode.J))
        {
            SmallItemSlot.ShowDescriptionPanel();
            MediumItemSlot.HideDescriptionPanel();
            LargeItemSlot.HideDescriptionPanel();
            choice = (int) Choices.Small;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SmallItemSlot.HideDescriptionPanel();
            MediumItemSlot.ShowDescriptionPanel();
            LargeItemSlot.HideDescriptionPanel();
            choice = (int) Choices.Medium;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SmallItemSlot.HideDescriptionPanel();
            MediumItemSlot.HideDescriptionPanel();
            LargeItemSlot.ShowDescriptionPanel();
            choice = (int) Choices.Large;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            HoldADTime += Time.deltaTime;
            HoldADFill.transform.localScale = new Vector3(HoldADTime,1,1);
            print("been holding for " + (int)HoldADTime);
            if (HoldADTime > 1f)
            {
                ChangeSceneScript.ChangeScene(nextScene);
            }
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            HoldADTime = 0;
            HoldADFill.transform.localScale = new Vector3(HoldADTime,1,1);
            print("been holding for " + (int)HoldADTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            HoldSpaceTime += Time.deltaTime;
            //don't stretch infinitely
            if(HoldSpaceTime < 1.01)
            {
                HoldSpaceFill.transform.localScale = new Vector3(HoldSpaceTime, 1, 1);
            }
            print("been holding for " + (int)HoldSpaceTime);
            if (HoldSpaceTime > 1f)
            {
                switch (choice)
                {
                    case (int) Choices.None:
                        error.Play();
                        print("no selection");
                        break;
                    case (int) Choices.Small:
                        SmallItemSlot.Buy();
                        break;
                    case (int) Choices.Medium:
                        MediumItemSlot.Buy();
                        break;
                    case (int) Choices.Large:
                        LargeItemSlot.Buy();
                        break;
                }
                HoldSpaceTime = 0f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            HoldSpaceTime = 0;
            HoldSpaceFill.transform.localScale = new Vector3(HoldSpaceTime,1,1);
            print("been holding for " + (int)HoldSpaceTime);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            SmallItemSlot.HideDescriptionPanel();
            MediumItemSlot.HideDescriptionPanel();
            LargeItemSlot.HideDescriptionPanel();
            //reset choice nothing
            choice = (int) Choices.None;
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
            if(smallItemsList[index] != null)
                SmallItemSlot.item = smallItemsList[index];

        //randomize Medium item slot
        index = Random.Range(0, mediumItemsList.Count());
        if(mediumItemsList.Any())
            if(mediumItemsList[index] != null)
                MediumItemSlot.item = mediumItemsList[index];

        //randomize Large item slot
        index = Random.Range(0, largeItemsList.Count());
        if(largeItemsList.Any())
            if(largeItemsList[index] != null)
                LargeItemSlot.item = largeItemsList[index];

        SmallItemSlot.Refresh();
        MediumItemSlot.Refresh();
        LargeItemSlot.Refresh();
    }

}
