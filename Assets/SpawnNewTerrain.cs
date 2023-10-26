using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SpawnNewTerrain : MonoBehaviour {
    public NavMeshSurface navMeshSurface;
    public GameObject terrainHolder;
    [SerializeField] GameObject[] arenaGameobjectIterations;
    [SerializeField] GameObject deadEndArena;

    [SerializeField] int maxArenasToPlace = 5;
    [SerializeField] GameObject[] arena;
    [SerializeField] List<GameObject> spawnPoints = new List<GameObject>();

    private void Awake() {
        // Initialize arena array with a size.
        arena = new GameObject[maxArenasToPlace];

        Debug.Log("SpawnNewTerrainScript");
        for(int arenasPlaced = 0; arenasPlaced <= maxArenasToPlace;) {
            
            if (arenasPlaced == 0) {
                Debug.Log("arena placed 0");
                arena[arenasPlaced] = Instantiate(arenaGameobjectIterations[Random.Range(0, arenaGameobjectIterations.Length)], Vector3.zero, Quaternion.identity, terrainHolder.transform);
                Debug.Log("Arena number: " +  arenasPlaced + " spawned");
                detectSpawnpoints(arena[arenasPlaced]);
                 
                foreach (GameObject spawnpoint in spawnPoints) {
                    arenasPlaced++;
                    arena[arenasPlaced] = Instantiate(arenaGameobjectIterations[Random.Range(0, arenaGameobjectIterations.Length)], spawnpoint.transform.position, spawnpoint.transform.rotation, terrainHolder.transform);
                    Debug.Log("Arena number: " + arenasPlaced + " spawned");
                }
                spawnPoints.Clear();

                for (int i = arenasPlaced; i <= maxArenasToPlace; i++) {
                    if (arena[i] != null) {
                        
                        detectSpawnpoints(arena[i].gameObject);
                        Debug.Log("Detecing spawnpoints for arena: " + i);
                    }
                    else if (arena[i] == null) {
                        break;
                    }
                }
            }
            else{
                Debug.Log("Arenas placed hiher than = 0 is: " + arenasPlaced);
                foreach (GameObject spawnpoint in spawnPoints) {
                    arenasPlaced++;
                    arena[arenasPlaced] = Instantiate(arenaGameobjectIterations[Random.Range(0, arenaGameobjectIterations.Length)], spawnpoint.transform.position, spawnpoint.transform.rotation, terrainHolder.transform);
                    Debug.Log("Arena number: " + arenasPlaced + " spawned");
                }
                spawnPoints.Clear();
                for (int i = arenasPlaced; i <= maxArenasToPlace; i++) {
                    if (arena[i] != null) {
                        detectSpawnpoints(arena[i].gameObject);
                        Debug.Log("Detecing spawnpoints for arena: " + i);
                    } else if (arena[i] == null) {
                        break;
                    }
                }
            }
        }
    }

    void detectSpawnpoints(GameObject spawnPointParent) {
        GameObject temp = spawnPointParent.transform.Find("Spawnpoints").gameObject;
        for (int i = 0; i < temp.transform.childCount; i++) {
                spawnPoints.Add(temp.transform.GetChild(i).gameObject);
        }
    }

    //GameObject[] spawnPoints = new GameObject[spawnPointsTransform.childCount];
    //            for (int i = 0; i<spawnPointsTransform.childCount; i++) {
    //                spawnPoints[i] = spawnPointsTransform.GetChild(i).gameObject;

    //// Rebuild the NavMesh after all terrain is spawned.
    //navMeshSurface.BuildNavMesh();

}

