using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    public AudioSource source;
    public AudioClip shootingAudioClip;

    
    void Start()
    {
        //Invoke("StartPlayingAudio", 3.0f);
        Invoke("OnTimerElapsed", 3.0f);
    }

    private void StartPlayingAudio()
    {
        source.PlayOneShot(shootingAudioClip);
        Invoke("OnTimerElapsed", shootingAudioClip.length);
    }

    private void OnTimerElapsed()
    {
        if(levelLoader != null)
            levelLoader.LoadNextScene();
    }
}
