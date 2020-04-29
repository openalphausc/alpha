using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class FeedbackMenu : MonoBehaviour
{
    public GameObject helpMenuUI;

    public GameObject feedbackMenuUI;

    public AudioMixer masterMixer;
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
            ReturnToHelp();
        }
    }

    public void OpenFeedback()
    {
        feedbackMenuUI.SetActive(true);
        helpMenuUI.SetActive(false);
    }
    
    public void ReturnToHelp()
    {
        feedbackMenuUI.SetActive(false);
        helpMenuUI.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        masterMixer.SetFloat("masterVolume", volume);
    }
    
    public void SetMusicVolume(float volume)
    {
        Debug.Log(volume);
        masterMixer.SetFloat("musicVolume", volume);
    }
    
    public void SetSFXVolume(float volume)
    {
        Debug.Log(volume);
        masterMixer.SetFloat("sfxVolume", volume);
    }
}