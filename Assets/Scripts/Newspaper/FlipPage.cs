using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlipPage : MonoBehaviour
{

    public GameObject page1; // Assign in inspector
    public GameObject page2;
    private bool pressed = false;


    // Start is called before the first frame update
    void Start()
    {
        //set pages
        page2.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //pressed tests to see if space has been pressed before or not
            //On first space press
            if (pressed == false)
            {
                page1.SetActive(false);
                page2.SetActive(true);
            }
            //On second space press
            if (pressed == true)
            {
                page2.SetActive(false);
                SceneManager.LoadScene("ShopScene");
            }
            //space has been pressed
            pressed = true;
        }
    }
}
