using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalManager : MonoBehaviour
{
    public AudioPlayer audioPlayer;
    public LevelLoader levelLoader;
    public AudioClip introClip;
    
    void Start()
    {
        StartCoroutine(PlayAudioForScene());
    }

    private IEnumerator PlayAudioForScene()
    {
        yield return new WaitForSeconds(3.0f); 
        yield return audioPlayer.PlayAudio(introClip);
        yield return new WaitForSeconds(2.0f); 
        levelLoader.LoadNextScene();
    }
}
