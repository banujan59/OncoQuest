using System.Collections;
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
        StartCoroutine(AudioPlayerCoroutine());
    }

    void Update()
    {
        UpdateAudio();

        bool isControllerInput = (OVRInput.GetConnectedControllers() & OVRInput.Controller.RTouch) == OVRInput.Controller.RTouch ||
        (OVRInput.GetConnectedControllers() & OVRInput.Controller.LTouch) == OVRInput.Controller.LTouch;
        
        if(isControllerInput && OVRInput.GetDown(goToNextLevelButton))
        {
            levelLoader.LoadNextScene();
        }
    }
    private void UpdateAudio()
    {
        if (OVRPlugin.userPresent)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.UnPause();
            }
        }
        else // Pause the audio if the user is not present
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    private IEnumerator AudioPlayerCoroutine()
    {
        audioSource.clip = roomEntryClip;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(roomEntryClip.length);

        // Wait for the user to wear the headset if necessary
        while (!OVRPlugin.userPresent)
        {
            yield return null;
        }

        yield return new WaitForSeconds(5.0f);

        while (true)
        {
            audioSource.clip = wbcClip;
            audioSource.Play();
            yield return new WaitForSecondsRealtime(wbcClip.length);

            // Wait for the user to wear the headset if necessary
            while (!OVRPlugin.userPresent)
            {
                yield return null;
            }

            yield return new WaitForSeconds(5.0f);
        }
    }
}
