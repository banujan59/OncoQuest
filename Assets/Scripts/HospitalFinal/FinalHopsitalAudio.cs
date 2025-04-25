using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopsitalAudioFinal : MonoBehaviour
{
    public AudioPlayer audioPlayer;
    public AudioClip congrats;


    void Start()
    {
        StartCoroutine(PlayAudioForScene());
    }

    private IEnumerator PlayAudioForScene()
    {
        yield return audioPlayer.PlayAudio(congrats);
    }
}
