using System.Collections;
using UnityEngine;

public class PassageAudio : MonoBehaviour
{
    public AudioPlayer audioPlayer;
    public AudioClip roomEntryClip;
    public AudioClip pressButtonToContinueClip;
    public OVRInput.RawButton goToNextLevelButton;
    public LevelLoader levelLoader;

    void Start()
    {
        StartCoroutine(PlayAudioForScene());
    }

    void Update()
    {
        bool isControllerInput = (OVRInput.GetConnectedControllers() & OVRInput.Controller.RTouch) == OVRInput.Controller.RTouch ||
        (OVRInput.GetConnectedControllers() & OVRInput.Controller.LTouch) == OVRInput.Controller.LTouch;
        
        if(isControllerInput && OVRInput.GetDown(goToNextLevelButton))
        {
            levelLoader.LoadNextScene();
        }
    }

    private IEnumerator PlayAudioForScene()
    {
        yield return audioPlayer.PlayAudio(roomEntryClip);
        yield return audioPlayer.PlayAudioUntilCondition(pressButtonToContinueClip);
    }
}
