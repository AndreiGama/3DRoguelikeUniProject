using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnNewTerrain : MonoBehaviour {
    [SerializeField] NavMeshSurface navMeshSurface;
    public GameObject terrainHolder;
    [SerializeField] GameObject[] arenaGameobjectIterations;
    [SerializeField] GameObject arena1PresetBeggining, arena2PresetBeggining, arena3PresetBeggining;
    [SerializeField] GameObject[] arenaPresetPreventLoop;
    [SerializeField] GameObject deadEndArena;
    [SerializeField] LayerMask terrainLayers;
    [SerializeField] int maxArenasToPlace = 5;
    GameObject[] arena;
    List<GameObject> spawnPoints = new List<GameObject>();
    List<GameObject> surplusArenas = new List<GameObject>();
    bool activatePreventLoop;
    bool preventLoop;
    int count;
    private void Awake() {
        // Initialize arena array with a size.
        arena = new GameObject[maxArenasToPlace];
        int lastArenaPlacedIndex;
        for (int arenasPlaced = 0; arenasPlaced <= maxArenasToPlace;) {


            if (arenasPlaced == 0) {
                arena[arenasPlaced] = Instantiate(arena1PresetBeggining, Vector3.zero, Quaternion.identity, terrainHolder.transform);

                CheckSlotValidity(arena[arenasPlaced].transform);
                lastArenaPlacedIndex = arenasPlaced;
                foreach (GameObject spawnpoint in spawnPoints) {
                    arenasPlaced++;
                    arena[arenasPlaced] = Instantiate(arena2PresetBeggining, spawnpoint.transform.position, spawnpoint.transform.rotation, terrainHolder.transform);
                }
                spawnPoints.Clear();
                for (int i = arenasPlaced; i > lastArenaPlacedIndex; i--) {
                    if (arena[i] != null) {
                        CheckSlotValidity(arena[i].transform);
                    } else if (arena[i] == null) {
                        break;
                    }
                }

            } else {
                //Checks for spawnpoints to be above 0 so it doesn't become an infinite loop
                if (spawnPoints.Count > 0) {
                    //Decreases count each time it loops back when it finds a possible infinite loop
                    if (count <= 0) {
                        preventLoop = false;
                    } else if (preventLoop == true) {
                        count--;
                    }
                    lastArenaPlacedIndex = arenasPlaced;
                    foreach (GameObject spawnpoint in spawnPoints) {
                        arenasPlaced++;
                        // Here is where it gets randomly selected or hard selected to make sure it doesn't run into a loop
                        GameObject arenaIterationSpawn = arenaGameobjectIterations[Random.Range(0, arenaGameobjectIterations.Length)];
                        // If the amount of arenas will be placed + the current amount of arenas generated are over the maximum arenas to place, it will bring a dead end arena to finish the build.
                        if (spawnPoints.Count + arenasPlaced >= maxArenasToPlace) {
                            surplusArenas.Add(Instantiate(deadEndArena, spawnpoint.transform.position, spawnpoint.transform.rotation, terrainHolder.transform));
                        } else {
                            if (preventLoop == true) {
                                if (count == 2) {
                                    arenaIterationSpawn = arenaPresetPreventLoop[0];
                                } else if (count == 1) {
                                    arenaIterationSpawn = arenaPresetPreventLoop[1];
                                }
                            }
                            // Instantaites object at spawnpoint
                            arena[arenasPlaced] = Instantiate(arenaIterationSpawn, spawnpoint.transform.position, spawnpoint.transform.rotation, terrainHolder.transform);
                            // Check for infinite loop
                            if (arenaIterationSpawn == arenaGameobjectIterations[0] || arenaIterationSpawn == arenaGameobjectIterations[1]) {
                                activatePreventLoop = true;
                            }
                        }

                    }
                    if (activatePreventLoop) {
                        preventLoop = true;
                        count = 3;
                        activatePreventLoop = false;
                    }
                    // Clears previous spawnpoints
                    spawnPoints.Clear();
                    // Loops through the arena we placed in order to check for new spawnpoints -- credit to james for the for loop algorithm we got shown in class...


                    for (int i = arenasPlaced; i > lastArenaPlacedIndex; i--) {
                        if (arena[i] != null) {
                            CheckSlotValidity(arena[i].transform);
                        } else if (arena[i] == null) {
                            break;
                        }
                    }


                } else {
                    // Sets arena placed to maximum because otherwise it infinite loops and freezes
                    arenasPlaced = maxArenasToPlace;
                    break;
                }
            }
        }
            if (spawnPoints.Count > 0) {
                foreach (GameObject spawnpoint in spawnPoints) {
                    surplusArenas.Add(Instantiate(deadEndArena, spawnpoint.transform.position, spawnpoint.transform.rotation, terrainHolder.transform));
                }
                spawnPoints.Clear();
            }
    }

    private void Start() {
        navMeshSurface.BuildNavMesh();
    }


    //Check if the slot the arena should spawn at is available or not
    void CheckSlotValidity(Transform spawnpointSlot) {
            Transform temp = spawnpointSlot.Find("Spawnpoints");
            for (int i = 0; i < temp.childCount; i++) {
                //Position the check will happen at
                Vector3 localPos = new Vector3(25, 0, 25);
                Vector3 worldPos = temp.GetChild(i).transform.TransformPoint(localPos);
                Collider[] overlappedObject = Physics.OverlapSphere(worldPos, 3f, terrainLayers);
                if (overlappedObject.Length == 0) {
                    spawnPoints.Add(temp.GetChild(i).gameObject);
                } else {
                    // Spot Taken
                }
            }
        }
    }


