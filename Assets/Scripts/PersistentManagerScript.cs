using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PersistentManagerScript : MonoBehaviour
{
    public static PersistentManagerScript Instance {get; private set;}

    public int money;
    [SerializeField] List<Item> inventory;

    private void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool Buy(Item item){
        if (money > item.price){
            print("Successful buy!");
            money -= item.price;
            inventory.Add(item);
            return true;
        }
        return false;
    }

    //returns an int, the total percentage speed increase players
    //should recieve from all their items
    public int invSpeedBonus(){
        int sum = 0;

        for (int i = 0; i < inventory.Count; i++)
        {   
            print("Adding the speed " + inventory[i].moveSpeed + " of " + inventory[i].name);
            sum += inventory[i].moveSpeed;
        }

        return sum;
    }
}
