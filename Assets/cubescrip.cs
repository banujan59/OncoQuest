using Unity.VisualScripting;
using UnityEngine;

public class cubescrip : MonoBehaviour
{
    [SerializeField]
    private Material EnterMaterial;

    [SerializeField]
    private GameObject m_grababbleWC;

    [SerializeField]
    private GameObject m_grababbleCC;

    private Material ogMaterial;

    private GameObject m_spawn1 = null;
    private GameObject m_spawn2 = null;

    void Start()
    {
        var cubeRenderer = GetComponent<Renderer>();
        ogMaterial = cubeRenderer.material;

        SpawnCells();
    }

    void Update()
    {
        if (m_spawn1 != null && m_spawn2 != null)
        {
            if (m_spawn1.IsDestroyed() && m_spawn2.IsDestroyed())
                SpawnCells();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material = EnterMaterial;

        SpawnCells();
    }

    void OnTriggerExit(Collider other)
    {
        var cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material = ogMaterial;
    }

    private void SpawnCells()
    {
        var spawnPosition = new Vector3(0, 0.4603f, 0.468f);
        Vector3 spawnOffset = Vector3.zero;
        m_spawn1 = Instantiate(m_grababbleWC, spawnPosition + spawnOffset, Quaternion.identity);
        m_spawn2 = Instantiate(m_grababbleCC, spawnPosition + spawnOffset + new Vector3(0.507f, 0, 0), Quaternion.identity);
    }
}
