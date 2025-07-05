using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubescrip : MonoBehaviour
{
    [SerializeField]
    private Material EnterMaterial;

    [SerializeField]
    private GameObject GrababbleCell;

    [SerializeField]
    private GameObject SplashObjectPrefab;

    private Material ogMaterial;

    // Start is called before the first frame update
    void Start()
    {
        var cubeRenderer = GetComponent<Renderer>();
        ogMaterial = cubeRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        var cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material = EnterMaterial;

        var parentTransform = GrababbleCell.transform;
        GameObject spawnedObject = Instantiate(SplashObjectPrefab, parentTransform.position, parentTransform.rotation);
        //spawnedObject.transform.SetParent(parentTransform);
        spawnedObject.transform.localPosition = new Vector3(0, -0.51f, 0);
        spawnedObject.transform.localScale = new Vector3(3f, 2.14f, 3f);
        GrababbleCell.SetActive(false);
    }

    void OnTriggerExit(Collider other)
    {
        var cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material = ogMaterial;
        GrababbleCell.SetActive(true);
    }
}
