using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SpawnNewTerrain : MonoBehaviour
{
    NavMeshSurface navMeshSurface;

    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject arenaGameobject;
    public bool playerTrigger;
    // Start is called before the first frame update
    void Start()
    {
        navMeshSurface = GameObject.FindWithTag("NavMesh").GetComponent<NavMeshSurface>();

        if (playerTrigger)
        {
            spawnTerrain();
            navMeshSurface.BuildNavMesh();
        }
    }

    void spawnTerrain()
    {
        foreach (GameObject spawnpoint in spawnPoints)
        {
            if (spawnpoint.activeSelf)
            {
                Instantiate(arenaGameobject, spawnpoint.transform.position, spawnpoint.transform.rotation);
            }
        }
    }
}
