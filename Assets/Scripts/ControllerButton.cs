using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButton : MonoBehaviour
{
    public AudioSource source;
    public AudioClip shootingAudioClip;

    public void FireProjectile()
    {
        source.PlayOneShot(shootingAudioClip);
    }
}
