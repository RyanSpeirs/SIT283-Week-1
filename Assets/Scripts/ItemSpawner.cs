using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Spawns items randomly from a set of configured types, each with its own
/// target count. Stops picking a type once it hits its target, and stops
/// spawning entirely once all types are done.

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ItemTypeConfig
    {
        public string typeName = "Item";
        public GameObject prefab;
        public int targetCount = 10;

        [HideInInspector] public int spawnedCount = 0;
    }

    [Header("Item Types")]
    public ItemTypeConfig[] itemTypes = new ItemTypeConfig[3];

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

    public void StartSpawning()
    {
        if (isSpawning) return;
        isSpawning = true;
        StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnRoutine()
    {
        while (isSpawning)
        {
            List<ItemTypeConfig> available = GetAvailableTypes();

            if (available.Count == 0)
            {
                Debug.Log("[ItemSpawner] All item type targets reached. Stopping spawner.");
                isSpawning = false;
                yield break;
            }

            ItemTypeConfig chosen = available[Random.Range(0, available.Count)];
            SpawnItem(chosen);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private List<ItemTypeConfig> GetAvailableTypes()
    {
        List<ItemTypeConfig> result = new List<ItemTypeConfig>();
        foreach (var type in itemTypes)
        {
            if (type.prefab != null && type.spawnedCount < type.targetCount)
                result.Add(type);
        }
        return result;
    }

    private void SpawnItem(ItemTypeConfig type)
    {
        Instantiate(type.prefab, spawnPoint.position, spawnPoint.rotation);
        type.spawnedCount++;
        Debug.Log($"[ItemSpawner] Spawned {type.typeName} ({type.spawnedCount}/{type.targetCount})");
    }
}