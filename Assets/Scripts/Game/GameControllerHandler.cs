using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    public OVRInput.RawButton shootingButton;
    public AudioPlayer audioPlayer;
    public LineRenderer linePrefab;
    public GameObject laserImpactPrefab;
    public Transform shootingPoint;
    public float maxLineDistance = 5;
    public float lineShowTimer = 0.3f;

    public AudioClip shootingAudioClip;
    public LayerMask layerMask;
    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        bool isControllerInput = (OVRInput.GetConnectedControllers() & OVRInput.Controller.RTouch) == OVRInput.Controller.RTouch ||
        (OVRInput.GetConnectedControllers() & OVRInput.Controller.LTouch) == OVRInput.Controller.LTouch;

        if(isControllerInput && OVRInput.GetDown(shootingButton))
        {
            FireProjectile();

            OVRHaptics.OVRHapticsChannel channel = OVRHaptics.LeftChannel; 
            
            if(shootingButton.ToString().Contains("RHandTrigger"))
                channel = OVRHaptics.RightChannel;

            PlayHapticFeedback(channel);
        }
    }
    private void PlayHapticFeedback(OVRHaptics.OVRHapticsChannel channel)
    {
        // Create a short haptic clip
        OVRHapticsClip hapticClip = new OVRHapticsClip();

        // Fill the clip with vibration data
        for (int i = 0; i < 200; i++) // Duration: ~0.06 seconds (320 samples per second)
        {
            hapticClip.WriteSample(255); // Maximum strength
        }

        // Play the haptic clip on the specified channel
        channel.Preempt(hapticClip);
    }

    public void FireProjectile()
    {   
        if(audioPlayer == null) // Do not allow shooting if we can't make a sound!
            return;

        audioPlayer.PlayOneShot(shootingAudioClip);

        Ray ray = new Ray(shootingPoint.position, shootingPoint.forward);
        bool hitTarget = Physics.Raycast(ray, out RaycastHit hit, maxLineDistance, layerMask);

        Vector3 endPoint;
        if(hitTarget)
        {
            endPoint = hit.point;

            Quaternion laserImpactRotation = Quaternion.LookRotation(-hit.normal);
            GameObject laserImpact = Instantiate(laserImpactPrefab, hit.point, laserImpactRotation);
            Destroy(laserImpact, 0.25f);

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
