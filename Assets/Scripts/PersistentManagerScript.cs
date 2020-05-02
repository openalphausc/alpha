using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PersistentManagerScript : MonoBehaviour
{
    //Treat "Instance" as the equivalent of "player" here. Player.Buy(item) == Instance.Buy(item).
    public static PersistentManagerScript Instance { get; private set; }

    public int levelIndex;
    public float levelProgress;
    
    //keeps lifetime track of the player's splits
    public List<float> floorSplits;

    //Keeps track of the player's current total currency.
    //think about edge cases when editing this directly:
    //If a player starts a round, wipes some stuff, and exits to the title screen or quits the game, restarts the level,
    //etc
    public int money;

    //A vector of items which holds all the items the player has
    //should make this private in the future, but useful to be public for debugging purposes in the editor
    public List<Item> inventory;
    //Consider adding other items such as record stats for display at end of rounds or in title "stats" screen

    public AudioSource citysounds;

    //This runs when the presistentManagerScript is first run, before "Start()" of all items, so that it will be non-nul
    //if called early in a scene
    private void Awake()
    {
        //dampen sound globally
        AudioListener.volume = 0.2f;
        //If there isn't an instance yet, create one
        if (Instance == null)
        {
            Instance = this;
            //and ensure it won't destroy when you load another scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //otherwise, destroy the gameObject trying to create another persistent manager instance (one exists)
            Destroy(gameObject);
        }
        citysounds.Play();
    }

    /**Buy function: Input the item to the inventory if player can afford it AND doesn't already have the item. Reduce
     * money by the item's price.
     * Returns false _if the item is already in the inventory_ or _can't afford_
     * Returns true if the purchase is successful
     */
    public bool Buy(Item item, AudioSource success, AudioSource error)
    {
        if (inventory.Contains(item))
        {
            error.Play();
            return false;
        }

        if (money < item.price)
        {
            error.Play();
            return false;
        }

        print("Successful buy!");
        success.Play();
        money -= item.price;
        inventory.Add(item);
        return true;
    }

    public void SkipTutorial()
    {
        PersistentManagerScript.Instance.levelIndex = 1;
    }


    //~~~~~~~~~~~~~~~~~~Modifiers section~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    /**
     * Returns true if the player has a watch or an item that acts as a watch.
     *
     */
    public bool InvWatch()
    {
        var watch = false;

        foreach (var item in inventory)
        {
            print("Checking the watch " + item.isWatch + " of " + item.name);
            if (item.isWatch)
            {
                watch = true;
            }
        }

        return watch;
    }

    /**
     * Returns true if the player has a watch or an item that acts as a watch.
     *
     */
    public bool InvHeadlamp()
    {
        var headlamp = false;

        foreach (var item in inventory)
        {
            print("Checking the headlamp " + item.isHeadlamp + " of " + item.name);
            if (item.isHeadlamp)
            {
                headlamp = true;
            }
        }

        return headlamp;
    }

    /**
     * Returns an int, the total percentage speed increase players
     * should receive from all their items.
     *
     */
    public int InvSpeedBonus()
    {
        var sum = 0;

        foreach (var item in inventory)
        {
            print("Adding the speed " + item.moveSpeed + " of " + item.name);
            sum += item.moveSpeed;
        }

        return sum;
    }

    /**
     * Returns an int, the total percentage refill speed increase
     * players should receive from all their items.
     */
    public int InvRefillSpeedBonus()
    {
        var sum = 0;

        foreach (var item in inventory)
        {
            print("Adding the speed " + item.refillSpeed + " of " + item.name);
            sum += item.refillSpeed;
        }

        return sum;
    }

    /**
     * Returns an int, the number of bonus sprays the player has from all their items.
     */
    public int InvSprayIncrease()
    {
        var sum = 0;

        foreach (var item in inventory)
        {
            print("Adding the increase " + item.sprayIncrease + " of " + item.name);
            sum += item.sprayIncrease;
        }

        return sum;
    }

    /**
     * Returns a double, the amount range increase the player has from all their items (default range is not yet set,
     * but will probably be approximately 10-15 % of the bar.
     */
    public double InvRefillRangeIncrease()
    {
        double sum = 0;

        foreach (var item in inventory)
        {
            print("Adding the refill range " + item.refillRange + " of " + item.name);
            sum += item.refillRange;
        }

        return sum;
    }

    /**
     * Returns a double, the time penalty reduction the player has from all their items.
     * This will effectively change the number that of TimeCleaningUp in GuageControl is set to (line circa 83 circa
     * 4/11/2020).
     */
    public double InvTimePenaltyReduction()
    {
        double sum = 0;

        foreach (var item in inventory)
        {
            print("Adding the time reductions " + item.timePenalties + " of " + item.name);
            sum += item.timePenalties;
        }

        return sum;
    }

    /**
     * Returns a double, the wipe range increase (default range is 3, max arm length 4)
     * the player has from all their items.
     * IMPORTANT: Be sure to change BOTH max_arm_length and wipe_range in wiper-controller AND targetRange in Character
     * Mover.
     */
    public double InvRangeIncrease()
    {
        double sum = 0;

        foreach (var item in inventory)
        {
            print("Adding the arm range increase " + item.rangeIncrease + " of " + item.name);
            sum += item.rangeIncrease;
        }

        return sum;
    }

    /**
     * Returns an int, the length of day increase the player has from all their items.
     */
    public int InvTimeIncrease()
    {
        var sum = 0;

        foreach (var item in inventory)
        {
            print("Adding the time increase " + item.timeIncrease + " of " + item.name);
            sum += item.timeIncrease;
        }

        return sum;
    }

    /**
     * Returns an int, the percentage income per floor increase the player has from all their items. Apply in
     * the start function of Wiper Controller in the assignment of minIncomePerFloor (feat-cash-shit branch)
     */
    public int InvIncomeIncrease()
    {
        var sum = 0;

        foreach (var item in inventory)
        {
            print("Adding the income increase " + item.incomeIncrease + " of " + item.name);
            sum += item.incomeIncrease;
        }

        return sum;
    }

    /**
     * Returns an int, the total bonus money the player gets for completion of a building
     * that the player has from all their items.
     */
    public int InvBonusOnCompletionIncrease()
    {
        var sum = 0;

        foreach (var item in inventory)
        {
            print("Adding the income increase " + item.bonusOnCompletionIncrease + " of " + item.name);
            sum += item.bonusOnCompletionIncrease;
        }

        return sum;
    }

    //#########################~~~~~END OF MODIFIERS SECTION~~~~~~~~##################################################//
}
