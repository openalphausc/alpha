using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PersistentManagerScript : MonoBehaviour
{
    //Treat "Instance" as the equivalent of "player" here. Player.Buy(item) == Instance.Buy(item).
    public static PersistentManagerScript Instance {get; private set;}
    
    //Keeps track of the player's current total currency.
    //think about edge cases when editing this directly:
    //If a player starts a round, wipes some stuff, and exits to the title screen or quits the game, restarts the level,
    //etc
    public int money;
    
    //A vector of items which holds all the items the player has
    //should make this private in the future, but useful to be public for debugging purposes in the editor
    [SerializeField] List<Item> inventory;
    //Consider adding other items such as record stats for display at end of rounds or in title "stats" screen
    
    //This runs when the presistentManagerScript is first run, before "Start()" of all items, so that it will be non-nul
    //if called early in a scene
    private void Awake()
    {
        //If there isn't an instance yet, create one
        if (Instance == null){
            Instance = this;
            //and ensure it won't destroy when you load another scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //otherwise, destroy the gameObject trying to create another persistent manager instance (one exists)
            Destroy(gameObject);
        }
    }
    
    //Buy function: Input the item you want the instance to buy
    //if player can afford it AND doesn't already have the item, purchase the item 
    //Returns false if the item is already in the inventory or can't afford
    //returns true if the purchase is successful
    public bool Buy(Item item){
        if (inventory.Contains(item))
        {
            return false;
        }
        if (money < item.price)
        {
            return false;
        }
        
        print("Successful buy!");
        money -= item.price;
        inventory.Add(item);
        return true;

    }

    
    //~~~~~~~~~~~~~~~~~~Modifiers section~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    /**
     * Returns true if the player has a watch or an item that acts as a watch
     * 
     */
    public bool InvWatch(){
        var watch = false;

        foreach (var item in inventory)
        {
            if (item.isWatch)
            {
                watch = true;
            }
        }

        return watch;
    }
    
    /**
     * Returns true if the player has a watch or an item that acts as a watch
     * 
     */
    public bool InvHeadlamp(){
        var headlamp = false;

        foreach (var item in inventory)
        {
            if (item.isHeadlamp)
            {
                headlamp = true;
            }
        }

        return headlamp;
    }
    
    /**
     * Returns an int, the total percentage speed increase players
     * should receive from all their items
     * 
     */
    public int InvSpeedBonus(){
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
     * players should receive from all their items
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
            print("Adding the speed " + item.sprayIncrease + " of " + item.name);
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
        var sum = 0;

        foreach (var item in inventory)
        {
            print("Adding the speed " + item.refillRange + " of " + item.name);
            sum += item.refillRange;
        }

        return sum;
    }
    
    //#########################~~~~~END OF MODIFIERS SECTION~~~~~~~~##################################################//
}
