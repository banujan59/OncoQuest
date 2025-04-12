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

    // UPDATE on audio
    public AudioSource audioSource;

    public AudioClip[] audioClips; 
    private int _currentClipIndex = 0;

    void Start()
    {
        // Get the camera's position and forward direction. This will be saved to respawn
        Transform cameraTransform = Camera.main.transform;
        _spawnCameraPosition = cameraTransform.position + new Vector3(0f, 1.0f, 0f);
        _spawnCameraForward = cameraTransform.forward;
        PlayNextClip();
    }

    void PlayNextClip()
    {
        if (_currentClipIndex < audioClips.Length)
        {
            if(_currentClipIndex == 1)
            {
                SpanObject(healthyCell);
            }

            else if(_currentClipIndex == 3)
            {
                DestroySpawnedObj();
                SpanObject(cancerCell);
            }

            audioSource.clip = audioClips[_currentClipIndex];
            audioSource.Play();
            Invoke("PlayNextClip", audioSource.clip.length);
            _currentClipIndex++;
        }

        else
        {
            SpanMultipleWIthOffset();
            Invoke("LoadNextLevel", 5.0f);
        }
    }

    private void SpanObject(GameObject objToSpawn)
    {
        const float spawnDistance = 0.5f;
        Vector3 spawnPosition = _spawnCameraPosition + _spawnCameraForward * spawnDistance;
        _currentSpawnedObj = Instantiate(objToSpawn, spawnPosition, Quaternion.identity);
        _currentSpawnedObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
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

        spawnPosition = _spawnCameraPosition + _spawnCameraForward * spawnDistance;
        spawnPosition += new Vector3(
                0.4f, 
                0f,
                0f // Keep the random offset on the Z-axis minimal
            );
        go = Instantiate(cancerCell, spawnPosition, Quaternion.identity);
        go.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    private void DestroySpawnedObj()
    {
        Destroy(_currentSpawnedObj);
    }

    private void LoadNextLevel()
    {
        levelLoader.LoadNextScene();
    }
}
