using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cancerCell;
    public GameObject healtyCell;
    public float spawnDistance = 5f; // Distance in front of the camera
    public float randomRange = 3f; // Range of random offset
    

    void Start()
    {
        SpawnObj(cancerCell, GetRandomSpawnCount());
        SpawnObj(healtyCell, GetRandomSpawnCount());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int GetRandomSpawnCount()
    {
        return Random.Range(9,15);
    }

    private void SpawnObj(GameObject objToSpawn, int count)
    {
        // Get the camera's position and forward direction
        Transform cameraTransform = Camera.main.transform;

        for(int i = 0 ; i < count ; i++)
        {
            // Calculate the position in front of the camera
            Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * spawnDistance;

            // Apply a random offset within the specified range
            spawnPosition += new Vector3(
                Random.Range(-1.0f, randomRange), // Random X offset
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
}
