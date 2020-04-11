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
        musicIntro.Play ();
        musicLoop.PlayDelayed(musicIntro.clip.length);
    }
    
}