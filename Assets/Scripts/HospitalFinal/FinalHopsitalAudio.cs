using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopsitalAudioFinal : MonoBehaviour
{
    public AudioSource source;
    public AudioClip congrats;


    void Start()
    {
        source.PlayOneShot(congrats);
    }
}
