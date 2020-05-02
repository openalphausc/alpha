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

    public string getItemDescription()
    {
        string itemDescription = "";

        if (isWatch)
        {
            itemDescription += "Lets you see the time. \n";
        }

        if (isHeadlamp)
        {
            itemDescription += "Lets you see in the dark. \n";
        }

        if (moveSpeed != 0)
        {
            itemDescription += "+";
            itemDescription += moveSpeed.ToString();
            itemDescription += "% Move speed \n";
            itemDescription += "Current: +";
            itemDescription += PersistentManagerScript.Instance.InvSpeedBonus().ToString();
            itemDescription += "% Move speed \n";
        }

        if (refillSpeed != 0)
        {
            itemDescription += "+";
            itemDescription += refillSpeed.ToString();
            itemDescription += "% Refill speed \n";
            itemDescription += "Current: +";
            itemDescription += PersistentManagerScript.Instance.InvRefillSpeedBonus().ToString();
            itemDescription += "% Refill speed \n";
        }

        if (sprayIncrease != 0)
        {
            itemDescription += "+";
            itemDescription += sprayIncrease.ToString();
            itemDescription += " Sprays per bottle \n";
            itemDescription += "Current: +";
            itemDescription += PersistentManagerScript.Instance.InvSprayIncrease().ToString();
            itemDescription += " Sprays per bottle \n";
        }

        if (refillRange != 0)
        {
            itemDescription += "+";
            itemDescription += refillRange.ToString();
            itemDescription += "% to refill range \n";
            itemDescription += "Current: +";
            itemDescription += PersistentManagerScript.Instance.InvRefillRangeIncrease().ToString();
            itemDescription += "% to refill range \n";
        }

        if (timePenalties != 0)
        {
            itemDescription += "-";
            itemDescription += timePenalties.ToString();
            itemDescription += "% to time penalties \n";
            itemDescription += "Current: -";
            itemDescription += PersistentManagerScript.Instance.InvTimePenaltyReduction().ToString();
            itemDescription += "% to time penalties \n";
        }

        if (rangeIncrease != 0)
        {
            itemDescription += "+";
            itemDescription += rangeIncrease.ToString();
            itemDescription += "% Reach increase \n";
            itemDescription += "Current: +";
            itemDescription += PersistentManagerScript.Instance.InvRangeIncrease().ToString();
            itemDescription += "% Reach increase \n";
        }

        if (timeIncrease != 0)
        {
            itemDescription += "+";
            itemDescription += timeIncrease.ToString();
            itemDescription += " to Time \n";
            itemDescription += "Current: +";
            itemDescription += PersistentManagerScript.Instance.InvTimeIncrease().ToString();
            itemDescription += " to Time \n";
        }

        if (incomeIncrease != 0)
        {
            itemDescription += "+";
            itemDescription += incomeIncrease.ToString();
            itemDescription += "% Income per floor\n";
            itemDescription += "Current: +";
            itemDescription += PersistentManagerScript.Instance.InvIncomeIncrease().ToString();
            itemDescription += "% Income per floor\n";
        }

        if (bonusOnCompletionIncrease != 0)
        {
            itemDescription += "+";
            itemDescription += bonusOnCompletionIncrease.ToString();
            itemDescription += " to Bonus on Completion \n";
            itemDescription += "Current: +";
            itemDescription += PersistentManagerScript.Instance.InvBonusOnCompletionIncrease().ToString();
            itemDescription += " to Bonus on Completion \n";
        }

        return itemDescription;
    }
}

