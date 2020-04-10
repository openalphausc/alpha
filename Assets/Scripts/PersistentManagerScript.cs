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
            money-=item.price;
            inventory.Add(item);
            return true;
        }
        return false;
    }

}
