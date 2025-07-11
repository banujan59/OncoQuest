using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    public GameObject cancerCell;
    public GameObject healtyCell;
    public float spawnDistance = 5f; // Distance in front of the camera
    public float randomRange = 4f; // Range of random offset

    private Vector3 _spawnCameraPosition;
    private Vector3 _spawnCameraForward;

    private int _nbCancerCells = 0;
    private int _nbHealtyCells = 0;
    private const string CANCER_CELL_TAG = "CancerCell";
    private const string HEALTY_CELL_TAG = "HealtyCell";

    private int _currentWave;

    public AudioPlayer audioPlayer;
    public AudioClip[] waveStartClips; 
    public AudioClip firstWBCClip1;    
    public AudioClip firstWBCClip2;   

    private bool _firstWBCHit = false;

    void Start()
    {
        // Get the camera's position and forward direction. This will be saved to respawn
        Transform cameraTransform = Camera.main.transform;
        _spawnCameraPosition = cameraTransform.position;
        _spawnCameraForward = cameraTransform.forward;

        _currentWave = 1;
        StartWave();
    }
    
    private void StartWave()
    {
        if (_currentWave == 1)
        {
            StartCoroutine(audioPlayer.PlayAudio(waveStartClips[0]));
        }

        _nbCancerCells = GetRandomSpawnCount();
        SpawnObj(cancerCell, _nbCancerCells);

        _nbHealtyCells = GetRandomSpawnCount();
        SpawnObj(healtyCell, _nbHealtyCells);
    }

    private int GetRandomSpawnCount()
    {
        return Random.Range(9,15);
    }

    private void SpawnObj(GameObject objToSpawn, int count)
    {
        for(int i = 0 ; i < count ; i++)
        {
            // Calculate the position in front of the camera
            Vector3 spawnPosition = _spawnCameraPosition + _spawnCameraForward * spawnDistance;

            // Apply a random offset within the specified range
            spawnPosition += new Vector3(
                Random.Range(-randomRange, randomRange), // Random X offset
                Random.Range(-1.0f, randomRange), // Random Y offset
                0f // Keep the random offset on the Z-axis minimal
            );

            // Rotate the position around the camera's origin to move it to the right side
            float rotationAngle = 0.0f; // Rotation angle in degrees to move to the right

            if(_currentWave >= 2)
            {
                List<float> possibleRotations = new List<float> {0.0f, -45.0f, 45.0f};
                int randomIndex = Random.Range(0, possibleRotations.Count);
                rotationAngle = possibleRotations[randomIndex];
            }

            Quaternion rotationAroundCamera = Quaternion.Euler(0, rotationAngle, 0);
            spawnPosition = Camera.main.transform.position + (rotationAroundCamera * (spawnPosition - Camera.main.transform.position));

            // Spawn the object at the calculated position and make it face the camera
            Quaternion rotationToFaceCamera = Quaternion.LookRotation(Camera.main.transform.position - spawnPosition);
            GameObject spawnedObject = Instantiate(objToSpawn, spawnPosition, rotationToFaceCamera);

            // Assign a random scale to the spawned object
            float randomScale = Random.Range(0.2f, 0.5f); // Random scale value between 0.2 and 0.5
            spawnedObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
    }

    public void HandleObjectDestroyed(GameObject cellToDestroy)
    {
        string objectTag = cellToDestroy.tag;


        if(objectTag == CANCER_CELL_TAG)
        {
            _nbCancerCells--;
        }

        else if (objectTag == HEALTY_CELL_TAG)
        {
            // Handle the first WBC hit if it's not already handled
            if (!_firstWBCHit)
            {
                _firstWBCHit = true;
                // Play two audio clips in sequence
                StartCoroutine(audioPlayer.PlayAudio(new List<AudioClip> {firstWBCClip1, firstWBCClip2}, 0.0f));
            }

            _nbHealtyCells--;
        }

        Destroy(cellToDestroy);

        if(_nbCancerCells == 0)
            EndWave();
    }

    private void EndWave()
    {
        GameObject[] remainingCells = GameObject.FindGameObjectsWithTag(HEALTY_CELL_TAG);

        // Print the names of the found objects
        foreach (GameObject obj in remainingCells)
        {
            Destroy(obj);
        }

        _currentWave++;

        if(_currentWave < 4)
            StartWave();

        else
            levelLoader.LoadNextScene();
    }
}
