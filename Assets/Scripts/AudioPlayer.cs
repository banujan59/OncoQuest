using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    private bool _continuePlaying = false;

    void Update()
    {
        if(audioSource == null)
            return;

        if (OVRPlugin.userPresent)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.UnPause();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    /// <summary>
    /// Function to play a sound one shot. Can play over the main sound playing by the PlayAudio function. 
    /// This function does not provide pause functionality for when the headset is removed. Use PlayAudio function instead.
    /// </summary>
    /// <param name="audioClip">The audio clip to be played.</param>
    public void PlayOneShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    /// <summary>
    /// Function to play a single audio clip. This function provides pause functionality for when the headset is removed. 
    /// However, it can only play one sound at a time. 
    /// </summary>
    /// <param name="audioClip">The audio clip to be played.</param>
    /// <returns></returns>
    public IEnumerator PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(audioClip.length);    

        // Wait for the user to wear the headset if necessary
        while(audioSource.isPlaying || !OVRPlugin.userPresent)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Function to play a list of audio clips.
    /// </summary>
    /// <param name="audioClips">The list of audio clips to play</param>
    /// <param name="pauseBetweenClips">Pause in seconds between each play. Default: 2 seconds</param>
    /// <returns></returns>
    public IEnumerator PlayAudio(List<AudioClip> audioClips, float pauseBetweenClips = 2.0f)
    {
        foreach(var audioClip in audioClips)
        {
            yield return StartCoroutine(PlayAudio(audioClip));
            yield return new WaitForSeconds(pauseBetweenClips);
        }
    }


    /// <summary>
    /// Function to play an audio clip indefinitely until a condition is set to false. Use the DisablePlayCondition() 
    /// function to disable the condition and stop playing the audio.
    /// </summary>
    /// <param name="audioClip">The audio clip to be played</param>
    /// <param name="pauseBetweenClips">Pause in seconds between each iteration. Default: 5 seconds</param>
    /// <returns></returns>
    public IEnumerator PlayAudioUntilCondition(AudioClip audioClip, float pauseBetweenClips = 5.0f)
    {
        _continuePlaying = true;
        while(_continuePlaying)
        {
            yield return StartCoroutine(PlayAudio(audioClip));
            yield return new WaitForSeconds(pauseBetweenClips);
        }
    }

    /// <summary>
    /// Function to disable the audio playing loop started in the PlayAudioUntilCondition function.
    /// </summary>
    public void DisablePlayCondition()
    {
        _continuePlaying = false;
    }
}
