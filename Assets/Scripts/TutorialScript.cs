using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public LevelLoader levelLoader;
    public GameObject cancerCell;
    public GameObject healtyCell;

    private Vector3 _spawnCameraPosition;
    private Vector3 _spawnCameraForward;

    private GameObject _currentSpawnedObj;

    private int spawnedObjIdx = 0;
    
    void Start()
    {
        // Get the camera's position and forward direction. This will be saved to respawn
        Transform cameraTransform = Camera.main.transform;
        _spawnCameraPosition = cameraTransform.position;
        _spawnCameraPosition += new Vector3(
            0f,
            1.0f, 
            0f
        );

        _spawnCameraForward = cameraTransform.forward;

        SpanObject(healtyCell);
    }

    private void SpanObject(GameObject objToSpawn)
    {
        const float spawnDistance = 0.5f;
        Vector3 spawnPosition = _spawnCameraPosition + _spawnCameraForward * spawnDistance;
        _currentSpawnedObj = Instantiate(objToSpawn, spawnPosition, Quaternion.identity);
        _currentSpawnedObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        Invoke("DestroySpawnedObj", 5.0f);
    }

    private void DestroySpawnedObj()
    {
        Destroy(_currentSpawnedObj);
        spawnedObjIdx++;

        if( spawnedObjIdx < 2)
            SpanObject(cancerCell);

        else
            levelLoader.LoadNextScene();

    }
}
