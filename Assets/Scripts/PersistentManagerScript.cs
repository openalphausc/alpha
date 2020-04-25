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

    //returns an int, the total percentage speed increase players
    //should receive from all their items
    public int InvSpeedBonus(){
        var sum = 0;

        foreach (var item in inventory)
        {
            print("Adding the speed " + item.moveSpeed + " of " + item.name);
            sum += item.moveSpeed;
        }

        return sum;
    }
}
