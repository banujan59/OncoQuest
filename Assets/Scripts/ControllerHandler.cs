using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    public OVRInput.RawButton shootingButton;
    public AudioSource source;
    public LineRenderer linePrefab;
    public Transform shootingPoint;
    public float maxLineDistance = 5;
    public float lineShowTimer = 0.3f;

    public AudioClip shootingAudioClip;
    public LayerMask layerMask;
    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(shootingButton))
        {
            FireProjectile();
        }
    }

     public void FireProjectile()
    {   
        source.PlayOneShot(shootingAudioClip);

        Ray ray = new Ray(shootingPoint.position, shootingPoint.forward);
        bool hitTarget = Physics.Raycast(ray, out RaycastHit hit, maxLineDistance, layerMask);

        Vector3 endPoint;
        if(hitTarget)
        {
            endPoint = hit.point;

            if(gameManager != null)
                gameManager.HandleObjectDestroyed(hit.collider.gameObject);
            else
                Destroy(hit.collider.gameObject);
        }

        else
        {
            endPoint = shootingPoint.position + shootingPoint.forward * maxLineDistance;
        }

        LineRenderer line = Instantiate(linePrefab);
        line.positionCount = 2;
        line.SetPosition(0, shootingPoint.position);

        line.SetPosition(1, endPoint);
        Destroy(line.gameObject, lineShowTimer);
    }

}
