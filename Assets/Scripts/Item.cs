using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int price;
    public string itemFlavorText;
    public bool isWatch;
    public bool isHeadlamp;
    public int moveSpeed;
    public int refillSpeed;
    public int sprayIncrease;
    public double refillRange;
    public double timePenalties;
    public double rangeIncrease;
    public int timeIncrease;
    public int incomeIncrease;
    public int bonusOnCompletionIncrease;

    public void loadDataFromName(string name) {
      Debug.Log("Loading sprite from name for item " + name);
      itemName = name;
      string filename = "Art/" + itemName;
      icon = Resources.Load<Sprite>(itemName);
    }

    public string getItemDescription()
    {
        string itemDescription = "";

        if (isWatch)
        {
            itemDescription += "Lets you see the time. \n";
        }

        if (isHeadlamp)
        {
            itemDescription += "Lets you see into the dark. \n";
        }
        
        if (moveSpeed != 0)
        {
            itemDescription += "+";
            itemDescription += moveSpeed.ToString();
            itemDescription += "% Move Speed \n";
        }

        if (refillSpeed != 0)
        {
            itemDescription += "+";
            itemDescription += refillSpeed.ToString();
            itemDescription += " to Refill Speed \n";
        }

        if (sprayIncrease != 0)
        {
            itemDescription += "+";
            itemDescription += sprayIncrease.ToString();
            itemDescription += " Sprays per Bottle \n";
        }

        if (refillRange != 0)
        {
            itemDescription += "+";
            itemDescription += refillRange.ToString();
            itemDescription += " to Refill Range \n";
        }

        if (timePenalties != 0)
        {
            itemDescription += "+";
            itemDescription += timePenalties.ToString();
            itemDescription += " to Time Penalties \n";
        }

        if (rangeIncrease != 0)
        {
            itemDescription += "+";
            itemDescription += rangeIncrease.ToString();
            itemDescription += " to Range \n";
        }

        if (timeIncrease != 0)
        {
            itemDescription += "+";
            itemDescription += timeIncrease.ToString();
            itemDescription += " to Time \n";
        }

        if (incomeIncrease != 0)
        {
            itemDescription += "+";
            itemDescription += incomeIncrease.ToString();
            itemDescription += " to Income \n";
        }

        if (bonusOnCompletionIncrease != 0)
        {
            itemDescription += "+";
            itemDescription += bonusOnCompletionIncrease.ToString();
            itemDescription += " to Bonus on Completion \n";
        }

        return itemDescription;
    }
}

