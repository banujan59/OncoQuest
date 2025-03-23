using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public LevelLoader levelLoader;
    public GameObject cancerCell;
    public GameObject healthyCell;

    private Vector3 _spawnCameraPosition;
    private Vector3 _spawnCameraForward;

    private GameObject _currentSpawnedObj;

    private int spawnedObjIdx = 0;

    // UPDATE on audio
    public AudioSource audioSource;
    public AudioClip roomEntryClip;
    public AudioClip wbcClip;
    public AudioClip pickupClip;
    public AudioClip cwbcClip;
    public AudioClip wrapUpClip; // NEW: Final wrap-up audio before transition

    void Start()
    {
        // Get the camera's position and forward direction. This will be saved to respawn
        Transform cameraTransform = Camera.main.transform;
        _spawnCameraPosition = cameraTransform.position + new Vector3(0f, 1.0f, 0f);
        _spawnCameraForward = cameraTransform.forward;

        // Play entry audio when entering the room
        if (audioSource != null && roomEntryClip != null)
            audioSource.PlayOneShot(roomEntryClip);

        Invoke("PlayWBCClip", 3.0f); // Schedule WBC explanation

        SpanObject(healthyCell, wbcClip);
    }

    private void PlayWBCClip()
    {
        if (audioSource != null && wbcClip != null)
            audioSource.PlayOneShot(wbcClip);

        Invoke("PlayPickupClip", 3.0f); // Schedule pickup explanation
    }

    private void PlayPickupClip()
    {
        if (audioSource != null && pickupClip != null)
            audioSource.PlayOneShot(pickupClip);
    }

    private void SpanObject(GameObject objToSpawn, AudioClip audioClip)
    {
        const float spawnDistance = 0.5f;
        Vector3 spawnPosition = _spawnCameraPosition + _spawnCameraForward * spawnDistance;
        _currentSpawnedObj = Instantiate(objToSpawn, spawnPosition, Quaternion.identity);
        _currentSpawnedObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        if (audioSource != null && audioClip != null)
            audioSource.PlayOneShot(audioClip);

        Invoke("DestroySpawnedObj", 5.0f);
    }

    private void DestroySpawnedObj()
    {
        Destroy(_currentSpawnedObj);
        spawnedObjIdx++;

        if (spawnedObjIdx < 2)
        {
            SpanObject(cancerCell, cwbcClip);
        }
        else
        {
            // Play wrap-up clip before transitioning
            if (audioSource != null && wrapUpClip != null)
            {
                audioSource.PlayOneShot(wrapUpClip);
                Invoke("LoadNextScene", wrapUpClip.length); // Wait for wrap-up to finish
            }
            else
            {
                LoadNextScene(); // Fallback if no audio
            }
        }
    }

    private void LoadNextScene()
    {
        levelLoader.LoadNextScene();
    }
}
