using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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


    private int currentLevel;
    
    void Start()
    {
        // Get the camera's position and forward direction. This will be saved to respawn
        Transform cameraTransform = Camera.main.transform;
        _spawnCameraPosition = cameraTransform.position;
        _spawnCameraForward = cameraTransform.forward;

        currentLevel = 1;
        StartWave();
    }
    
    private void StartWave()
    {
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

            // Spawn the object at the calculated position and with no rotation
            GameObject spawnedObject = Instantiate(objToSpawn, spawnPosition, Quaternion.identity);
        
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

        else if(objectTag == HEALTY_CELL_TAG)
        {
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

        currentLevel++;

        if(currentLevel < 6)
            StartWave();

        // else go back to hospital scene
    }
}
