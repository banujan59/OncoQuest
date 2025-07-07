using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_splashPrefab;

    void OnTriggerEnter(Collider other)
    {
        var otherGo = other.gameObject;
        Transform current = otherGo.transform;

        while (current.parent != null)
        {
            current = current.parent;
        }

        Debug.Log("collusion with : " + other.gameObject.name);

        //if (current.gameObject == m_cameraRig)
        //if (current.gameObject.name == "[BuildingBlock] Camera Rig")
        //if (current.gameObject == m_cameraRig)
        if (current.gameObject.name == "[BuildingBlock] Camera Rig")
            return;

        Vector3 bottomOfThisObject = new Vector3(
            transform.position.x,
            GetComponent<Collider>().bounds.min.y,
            transform.position.z
        );

        Vector3 contactPoint = other.ClosestPoint(bottomOfThisObject);
        Instantiate(m_splashPrefab, contactPoint, Quaternion.identity);
        Destroy(gameObject);
    }
}
