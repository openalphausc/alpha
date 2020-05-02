using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlipPage : MonoBehaviour
{

    public GameObject page1; // Assign in inspector
    private bool pressed = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("ShopScene");
        }
    }
}
