using System.Collections;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    // Serializable class to hold data for spawning each NPC
    [System.Serializable]
    public class NPCSpawnData
    {
        public GameObject NPCGameObject; // Prefab of the NPC to spawn
        public float spawnDelay = 10f; // Delay before spawning the NPC
    }

    public NPCSpawnData[] npcSpawnDataArray; // Array of NPC spawn data

    void Start()
    {
        // Start spawning NPCs
        StartCoroutine(SpawnNPCs());
    }

    // Coroutine to spawn NPCs
    IEnumerator SpawnNPCs()
    {
        // Iterate through each NPC spawn data
        foreach (NPCSpawnData npcSpawnData in npcSpawnDataArray)
        {
            GameObject NPCGameObject = npcSpawnData.NPCGameObject;
            float spawnDelay = npcSpawnData.spawnDelay;

            // Deactivate the NPC initially
            NPCGameObject.SetActive(false);

            // Start a new coroutine to spawn NPC after the specified delay
            StartCoroutine(SpawnNPCAfterDelay(NPCGameObject, spawnDelay));
        }

        // End the main coroutine
        yield break;
    }

    // Coroutine to spawn NPC after a delay
    IEnumerator SpawnNPCAfterDelay(GameObject NPCGameObject, float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);

        // Activate the NPC after the delay
        NPCGameObject.SetActive(true);
    }
}
