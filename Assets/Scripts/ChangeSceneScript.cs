using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    public string sceneName;
    public void PlayGame()
    {
        SaveLoader.LoadGame();
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public static void ChangeScene(string sceneIn)
    {
        SceneManager.LoadScene(sceneIn);
    }
}
