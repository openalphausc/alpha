using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene("BuildingScene");
    }

   void Update()
   {
       if (Input.GetKeyDown(KeyCode.Space))
       {
           SceneManager.LoadScene("BuildingScene");
       }
   }
}
