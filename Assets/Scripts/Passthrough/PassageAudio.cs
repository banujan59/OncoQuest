using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip roomEntryClip;
    public AudioClip wbcClip;
    public OVRInput.RawButton goToNextLevelButton;
    public LevelLoader levelLoader;

    void Start()
    {
        PlayAudioAndSpawn();
    }

    void Update()
    {
        bool isControllerInput = (OVRInput.GetConnectedControllers() & OVRInput.Controller.RTouch) == OVRInput.Controller.RTouch ||
        (OVRInput.GetConnectedControllers() & OVRInput.Controller.LTouch) == OVRInput.Controller.LTouch;
        
        if(isControllerInput && OVRInput.GetDown(goToNextLevelButton) && isControllerInput)
        {
            levelLoader.LoadNextScene();
        }
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
