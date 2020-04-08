using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite Icon;
    public int price;
    public string ItemDescription;
    public string ItemFlavorText;
    public bool isWatch;
    public bool isHeadlamp;
    public int moveSpeed;
    public int refillSpeed;
    public int sprayIncrease;
    public int refillRange;
    public int timePenalties;
    public int rangeIncrease;
    public int timeIncrease;
    public int incomeIncrease;
    
}