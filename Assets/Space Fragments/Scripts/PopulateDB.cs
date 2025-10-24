using Fragments.Runtime;
using UnityEngine;

public class PopulateDB : MonoBehaviour
{
    // Method to populate ResourceDB from a spawner
    public ResourceDB db;
    public AreaSpawner2D spawner;

    public void Start()
    {
        Populate(db, spawner);
    }
    public void Populate(ResourceDB db, AreaSpawner2D spawner)
    {
        if (db == null || spawner == null) return;

        db.resources.Clear();

        foreach (var prefab in spawner.orePrefabs)
        {
            if (prefab == null) continue;

            ResourceInfo info = new ResourceInfo();
            info.id = prefab.name;
            info.displayName = prefab.name;
            info.weight = 10f;
            info.prefab = prefab;

            db.resources.Add(info);
        }

        Debug.Log($"Populated DB with {db.resources.Count} resources from spawner.");
    }
}