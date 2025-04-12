using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageAudio : MonoBehaviour
{
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
    }
}
