using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageAudio : MonoBehaviour
{
    public LevelLoader levelLoader;

    public AudioSource audioSource;
    public AudioClip roomEntryClip;
    public AudioClip wbcClip;

    void Start()
    {
        PlayAudioAndSpawn();
    }

    void PlayAudioAndSpawn()
    {
        audioSource.PlayOneShot(roomEntryClip);
        Invoke("PlayWBCClip", roomEntryClip.length); 
    }

    void PlayWBCClip()
    {
        audioSource.PlayOneShot(wbcClip);
        Invoke("LoadNextScene", wbcClip.length); 
    }

    void LoadNextScene()
    {
        levelLoader.LoadNextScene();
    }
}
