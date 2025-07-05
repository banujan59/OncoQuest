using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_splashObjectPrefab;

    [SerializeField]
    private GameObject m_cellPrefab;

    [SerializeField]
    private GameObject m_parentGo;

    void OnTriggerEnter(Collider other)
    {
        var otherGo = other.gameObject;
        Transform current = otherGo.transform;

        while (current.parent != null)
        {
            current = current.parent;
        }

        Debug.Log("In trigger! Collided with: " + current.gameObject.name);
        if (current.gameObject == m_parentGo)
            return;


        // Stop all movement and rotation
        var rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll; // Optionally, freeze the object completely

        var otherTransform = other.gameObject.transform;
        GameObject spawnedObject = Instantiate(m_splashObjectPrefab, otherTransform.position, otherTransform.rotation);
        spawnedObject.transform.SetParent(transform);
        spawnedObject.transform.localPosition = new Vector3(0, -0.51f, 0);
        spawnedObject.transform.localScale = new Vector3(3f, 2.14f, 3f);

        m_cellPrefab.SetActive(false);
    }
}
