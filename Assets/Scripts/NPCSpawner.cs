using System.Collections;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [System.Serializable]
    public class NPCSpawnData
    {
        public GameObject NPCGameObject;
        public float spawnDelay = 10f;
    }

    public NPCSpawnData[] npcSpawnDataArray;

    void Start()
    {
        StartCoroutine(SpawnNPCs());
    }

    IEnumerator SpawnNPCs()
    {
        foreach (NPCSpawnData npcSpawnData in npcSpawnDataArray)
        {
            GameObject NPCGameObject = npcSpawnData.NPCGameObject;
            float spawnDelay = npcSpawnData.spawnDelay;

            NPCGameObject.SetActive(false);

            // Start a new coroutine for each NPC spawn
            StartCoroutine(SpawnNPCAfterDelay(NPCGameObject, spawnDelay));
        }

        yield break; // End the main coroutine
    }

    IEnumerator SpawnNPCAfterDelay(GameObject NPCGameObject, float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        NPCGameObject.SetActive(true);
    }
}