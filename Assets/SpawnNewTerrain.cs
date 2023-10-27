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
    [SerializeField] LayerMask terrainLayers;
    [SerializeField] int maxArenasToPlace = 5;
    [SerializeField] GameObject[] arena;
    [SerializeField] List<GameObject> spawnPoints = new List<GameObject>();

    private void Start() {
        // Initialize arena array with a size.
        arena = new GameObject[maxArenasToPlace];

        Debug.Log("SpawnNewTerrainScript");
        for(int arenasPlaced = 0; arenasPlaced <= maxArenasToPlace;) {
            
            if (arenasPlaced == 0) {
                Debug.Log("arena placed 0");
                arena[arenasPlaced] = Instantiate(arenaGameobjectIterations[Random.Range(0, arenaGameobjectIterations.Length)], Vector3.zero, Quaternion.identity, terrainHolder.transform);
                Debug.Log("Arena number: " +  arenasPlaced + " spawned");
                 
                foreach (GameObject spawnpoint in spawnPoints) {
                    arenasPlaced++;
                    arena[arenasPlaced] = Instantiate(arenaGameobjectIterations[Random.Range(0, arenaGameobjectIterations.Length)], spawnpoint.transform.position, spawnpoint.transform.rotation, terrainHolder.transform);
                    Debug.Log("Arena number: " + arenasPlaced + " spawned");
                }
                spawnPoints.Clear();

                for (int i = arenasPlaced; i <= maxArenasToPlace; i++) {
                    if (arena[i] != null) {
                        
                        checkSlotValidity(arena[i].transform);
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
                        checkSlotValidity(arena[i].transform);
                        Debug.Log("Detecing spawnpoints for arena: " + i);
                    } else if (arena[i] == null) {
                        break;
                    }
                }
            }
        }
    }

    //void detectSpawnpoints(Transform spawnPointParent) {
    //    Transform temp = spawnPointParent.Find("Spawnpoints");
    //    for (int i = 0; i < temp.childCount; i++) {
    //        Debug.Log("child" + temp.GetChild(i).name);
    //        spawnPoints.Add(temp.GetChild(i).gameObject);
    //    }
    //}

    void checkSlotValidity(Transform spawnpointSlot) {
        Transform spawnpointsT = spawnpointSlot.Find("Spawnpoints");
        for(int i = 0; i < spawnpointsT.childCount; i++) {
            GameObject child = spawnpointsT.GetChild(i).gameObject;
            Vector3 localPos = new Vector3(25, 1, 25);
            Vector3 worldPos = child.transform.TransformPoint(localPos);
            if (Physics.CheckSphere(worldPos, 3f, terrainLayers, QueryTriggerInteraction.Collide)) {
                Debug.Log("Spot taken");
                Debug.Log(child.name);
                Destroy(child);
            } else {
                spawnPoints.Add(child);
                Debug.Log("Empty spot");

            }
        } // this frezes the function when it can't find any spawnpoints to add
        //detectSpawnpoints(spawnPoint);
    }
}

