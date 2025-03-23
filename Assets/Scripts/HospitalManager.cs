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
        //source.PlayOneShot(shootingAudioClip);
        Invoke("OnTimerElapsed", 5.0f);
    }

    void OnTimerElapsed()
    {
        if(levelLoader != null)
            levelLoader.LoadNextScene();
    }
}
