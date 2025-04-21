using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public AudioPlayer audioPlayer;
    public LevelLoader levelLoader;
    public GameObject cancerCell;
    public GameObject healthyCell;
    public AudioClip[] tutorialClips; 

    public AudioClip pickUpControllerClip;
    public AudioClip pressButtonToContinueClip;
    public OVRInput.RawButton goToNextLevelButton;

    private Vector3 _spawnCameraPosition;
    private Vector3 _spawnCameraForward;
    private List<GameObject> _spawnedObjects = new();
    private bool _waitForInputFromController = false;

    void Start()
    {
        // Get the camera's position and forward direction. This will be saved to respawn
        Transform cameraTransform = Camera.main.transform;
        _spawnCameraPosition = cameraTransform.position + new Vector3(0f, 1.0f, 0f);
        _spawnCameraForward = cameraTransform.forward;

        StartCoroutine(PlayAudioForScene());
    }

    private void Update()
    {
        if(!_waitForInputFromController)
            return;

        bool isControllerInput = (OVRInput.GetConnectedControllers() & OVRInput.Controller.RTouch) == OVRInput.Controller.RTouch ||
        (OVRInput.GetConnectedControllers() & OVRInput.Controller.LTouch) == OVRInput.Controller.LTouch;
        
        if(isControllerInput && OVRInput.GetDown(goToNextLevelButton))
            levelLoader.LoadNextSceneWithFadeColor(Color.white);
    }

    private IEnumerator PlayAudioForScene()
    {
        for(int currentClipIndex = 0 ; currentClipIndex < tutorialClips.Length ; currentClipIndex++)
        {
            if(currentClipIndex == 1)
            {
                SpanObject(healthyCell);
            }

            else if(currentClipIndex == 3)
            {
                DestroySpawnedObj();
                SpanObject(cancerCell);
            }

            yield return audioPlayer.PlayAudio(tutorialClips[currentClipIndex]);
        }

        SpanMultipleWIthOffset();

        yield return new WaitForSeconds(5.0f);
        DestroySpawnedObj();
        yield return audioPlayer.PlayAudio(pickUpControllerClip);
        _waitForInputFromController = true;
        yield return audioPlayer.PlayAudioUntilCondition(pressButtonToContinueClip);
    }

    private void SpanObject(GameObject objToSpawn)
    {
        const float spawnDistance = 0.5f;
        Vector3 spawnPosition = _spawnCameraPosition + _spawnCameraForward * spawnDistance;
        GameObject go = Instantiate(objToSpawn, spawnPosition, Quaternion.identity);
        go.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        _spawnedObjects.Add(go);
    }

    private void SpanMultipleWIthOffset()
    {
        const float spawnDistance = 0.5f;

        Vector3 spawnPosition = _spawnCameraPosition + _spawnCameraForward * spawnDistance;
        spawnPosition += new Vector3(
                -0.4f,
                0f,
                0f // Keep the random offset on the Z-axis minimal
            );
        GameObject go = Instantiate(cancerCell, spawnPosition, Quaternion.identity);
        go.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        _spawnedObjects.Add(go);

        spawnPosition = _spawnCameraPosition + _spawnCameraForward * spawnDistance;
        spawnPosition += new Vector3(
                0.4f, 
                0f,
                0f // Keep the random offset on the Z-axis minimal
            );
        go = Instantiate(cancerCell, spawnPosition, Quaternion.identity);
        go.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        _spawnedObjects.Add(go);
    }

    private void DestroySpawnedObj()
    {
        foreach(GameObject obj in _spawnedObjects)
        {
            Destroy(obj);
        }

        _spawnedObjects.Clear();
    }
}
