using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float intensity = 0.00005f; // Intensity of vibration
    public float speed = 0.5f;        // Speed of vibration (lower values = slower movement)

    private Vector3 originalPosition; // Store the original position
    private Vector2 randomOffset;     // Unique random offset for each object

    void Start()
    {
        // Save the starting position
        originalPosition = transform.position;

        // Generate a unique random offset for each object
        randomOffset = new Vector2(Random.Range(0f, 100f), Random.Range(0f, 100f));
    }

    void Update()
    {
        // Use Perlin Noise with the unique random offsets to ensure independent movement
        float offsetX = Mathf.PerlinNoise(Time.time * speed + randomOffset.x, 0f) * 2f - 1f;
        float offsetY = Mathf.PerlinNoise(0f, Time.time * speed + randomOffset.y) * 2f - 1f;

        // Scale the offsets by intensity
        offsetX *= intensity;
        offsetY *= intensity;

        // Apply the random offsets to the original position
        transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);
    }
}
