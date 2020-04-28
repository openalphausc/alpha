using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(AudioSource))]
public class LoopTitleMusic: MonoBehaviour
{
    public AudioSource musicIntro;
    public AudioSource musicLoop;
    private bool startedLoop;

    void Start()
    {
        //Dampen the sound of the game
        //AudioListener.volume = 0.1f;
        musicIntro.Play();
        musicLoop.PlayDelayed(musicIntro.clip.length);
    }
    
}