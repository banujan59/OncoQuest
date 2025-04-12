using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public AudioPlayer audioPlayer;
    public LevelLoader levelLoader;
    public GameObject cancerCell;
    public GameObject healthyCell;

    private Vector3 _spawnCameraPosition;
    private Vector3 _spawnCameraForward;

    private GameObject _currentSpawnedObj;

    public AudioClip[] audioClips; 

    void Start()
    {
        // Get the camera's position and forward direction. This will be saved to respawn
        Transform cameraTransform = Camera.main.transform;
        _spawnCameraPosition = cameraTransform.position + new Vector3(0f, 1.0f, 0f);
        _spawnCameraForward = cameraTransform.forward;

        StartCoroutine(PlayAudioForScene());
    }

    private IEnumerator PlayAudioForScene()
    {
        for(int currentClipIndex = 0 ; currentClipIndex < audioClips.Length ; currentClipIndex++)
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

            yield return audioPlayer.PlayAudio(audioClips[currentClipIndex]);
        }

        SpanMultipleWIthOffset();
        yield return new WaitForSeconds(5.0f);
        levelLoader.LoadNextScene();
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
}
