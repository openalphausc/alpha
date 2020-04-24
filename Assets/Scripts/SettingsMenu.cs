using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SettingsMenu : MonoBehaviour
{
    [FormerlySerializedAs("MainMenuUI")] public GameObject mainMenuUi;

    [FormerlySerializedAs("SettingsMenuUI")] public GameObject settingsMenuUi;

    public AudioMixer masterMixer;
    public AudioMixer soundMixer;
    public AudioMixer sfxMixer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayGame()
    {
        SaveLoader.LoadGame();
        SceneManager.LoadScene("TutorialScene");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting ...");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMain();
        }
    }

    public void OpenSettings()
    {
        settingsMenuUi.SetActive(true);
        mainMenuUi.SetActive(false);
    }
    
    public void ReturnToMain()
    {
        settingsMenuUi.SetActive(false);
        mainMenuUi.SetActive(true);
    }

    public void setVolume(float volume)
    {
        Debug.Log(volume);
        masterMixer.SetFloat("masterVolume", volume);
    }
}