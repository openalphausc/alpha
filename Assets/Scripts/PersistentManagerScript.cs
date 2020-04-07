using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PersistentManagerScript : MonoBehaviour
{
    public static PersistentManagerScript Player {get; private set;}

    public int money;

    private void Awake()
    {
        if (Player == null){
            Player = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
