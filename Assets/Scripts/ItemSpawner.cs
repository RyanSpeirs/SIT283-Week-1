using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Spawns items randomly from a set of types
public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ItemTypeConfig
    {
        public ItemType type;
        public GameObject prefab;
        //  This caused errors in playmode due to cached data carried over
        public bool stopped = false;
    }

    // Sets the item types
    [Header("Item Types")]
    public ItemTypeConfig[] itemTypes = new ItemTypeConfig[3];

    //  as the header says, lets us adjust the spawn settings
    [Header("Spawn Settings")]
    public Transform spawnPoint;
    public float spawnInterval = 2f;
    public bool autoStart = true;

    private bool isSpawning = false;

    private void Start()
    {
        if (spawnPoint == null) spawnPoint = transform;
        if (autoStart) StartSpawning();
    }

    // Does what it says, starts the item spawning logic
    public void StartSpawning()
    {
        if (isSpawning) return;
        isSpawning = true;
        StartCoroutine(SpawnRoutine());
    }

    //  Tells our spawner to stop, in theory
    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    // Triggered by ItemBin's OnBinFull event to stop spawning an item type, in theory
    public void ForceStopType(ItemType type)
    {
        Debug.Log($"ForceStopType called for {type}");
        foreach (var t in itemTypes)
        {
            if (t.type == type)
                t.stopped = true;
        }
    }

    // The headline method, this will pick from items and spawn them
    private IEnumerator SpawnRoutine()
    {
        while (isSpawning)
        {
            List<ItemTypeConfig> available = GetAvailableTypes();

            if (available.Count == 0)
            {
                Debug.Log("[ItemSpawner] All item types stopped. Stopping spawner.");
                isSpawning = false;
                yield break;
            }
            // Randomly selects from the 3 different items
            ItemTypeConfig chosen = available[Random.Range(0, available.Count)];
            SpawnItem(chosen);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // list of items that have been registered with the spawner
    private List<ItemTypeConfig> GetAvailableTypes()
    {
        List<ItemTypeConfig> result = new List<ItemTypeConfig>();
        foreach (var type in itemTypes)
        {
            Debug.Log($"[Check] {type.type}: stopped={type.stopped}");
            if (type.prefab != null && !type.stopped)
                result.Add(type);
        }
        return result;
    }

    //  the purpose spawns items at a designated point.
    private void SpawnItem(ItemTypeConfig type)
    {
        GameObject obj = Instantiate(type.prefab, spawnPoint.position, spawnPoint.rotation);

        Item item = obj.GetComponent<Item>();
        if (item != null) item.type = type.type;

        Debug.Log($"[ItemSpawner] Spawned {type.type}");
    }
}