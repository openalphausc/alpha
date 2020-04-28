using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUi;
    public GameObject settingsMenuUi;
    public GameObject statsMenuUI;
    public GameObject helpMenuUI;
    public GameObject feedbackMenuUI;
    public TMP_Text versionText;
    // Start is called before the first frame update
    void Start()
    {
        versionText.text = "v" + Application.version;
    }

    public void PlayGame()
    {
        SaveLoader.LoadGame();
        SceneManager.LoadScene("TutorialScene");
    }
    
    public void ChangeScene(string scene)
    {
        SaveLoader.LoadGame();
        SceneManager.LoadScene(scene);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting ...");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TutorialScene");
        }
    }

    public void OpenSettings()
    {
        settingsMenuUi.SetActive(true);
        mainMenuUi.SetActive(false);
    }
    
    public void OpenStats()
    {
        statsMenuUI.SetActive(true);
        mainMenuUi.SetActive(false);
    }
    
    public void OpenHelp()
    {
        helpMenuUI.SetActive(true);
        mainMenuUi.SetActive(false);
    }
    
    public void OpenFeedback()
    {
        feedbackMenuUI.SetActive(true);
        mainMenuUi.SetActive(false);
        helpMenuUI.SetActive(false);
    }
    
    public void ReturnToMain()
    {
        settingsMenuUi.SetActive(false);
        helpMenuUI.SetActive(false);
        statsMenuUI.SetActive(false);
        mainMenuUi.SetActive(true);
    }
}