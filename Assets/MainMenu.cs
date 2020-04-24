using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public GameObject MainMenuUI;
    public GameObject SettingsMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void PlayGame()
    {
        SaveLoader.LoadGame();
        SceneManager.LoadScene("TutorialScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayGame();
        }
    }

}