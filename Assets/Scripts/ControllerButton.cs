using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButton : MonoBehaviour
{
    public AudioSource source;
    public LineRenderer linePrefab;
    public Transform shootingPoint;
    public float maxLineDistance = 5;
    public float lineShowTimer = 0.3f;

    public AudioClip shootingAudioClip;

    public void FireProjectile()
    {
        source.PlayOneShot(shootingAudioClip);
        LineRenderer line = Instantiate(linePrefab);
        line.positionCount = 2;
        line.SetPosition(0, shootingPoint.position);

        Vector3 endPoint = shootingPoint.position + shootingPoint.forward * maxLineDistance;
        line.SetPosition(1, endPoint);
        Destroy(line.gameObject, lineShowTimer);
    }
}
