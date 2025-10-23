using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawningControl : MonoBehaviour
{
    public GameObject junkPrefab;
    public Transform player;

    //for the spawning system im going to be breaking the world into chunks

    [Header("Cluster Settings")] // headers to make it easier to controll in unity
    public int junkPerClusterMin = 5;
    public int junkPerClusterMax = 15;
    public float clusterRadius = 40f; //must be kept below half of chunk size to avoid overlap
    public int JunkChancemax = 10; //higher the number lower the chace of cluster spawning // => 10% chance for cluster, 30% for small junk, 60% empty
    public int SmallJunkChance = 4; //chance of spawning 1-3 peices (KEEP BELOW MAX+1) // number - 1 is the chance
    public int JunkCount = 0; //this is being used for naming, and possible deleting in future


    [Header("Spawning Control")] 
    public float chunkSize = 20f;
    public int chunkViewDistance = 2;

    private Vector2Int currentChunkCoord;
    private HashSet<Vector2Int> spawnedChunks = new HashSet<Vector2Int>();

    void Start()
    {
        currentChunkCoord = GetChunkCoord(player.position); 
        SpawnVisibleChunks();
    }

    void Update()
    {
        Vector2Int newChunkCoord = GetChunkCoord(player.position);

        if (newChunkCoord != currentChunkCoord)
        {
            currentChunkCoord = newChunkCoord;
            SpawnVisibleChunks();
        }
    }

    Vector2Int GetChunkCoord(Vector2 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / chunkSize),
            Mathf.FloorToInt(position.y / chunkSize)
        );
    }

    void SpawnVisibleChunks()
    {
        for (int x = -chunkViewDistance; x <= chunkViewDistance; x++) //spawns on both sides
        {
            for (int y = -chunkViewDistance; y <= chunkViewDistance; y++) //spawns up and down(if it can)
            {
                Vector2Int chunkCoord = currentChunkCoord + new Vector2Int(x, y);

                if (!spawnedChunks.Contains(chunkCoord)) //basically if new chunk
                {
                    spawnedChunks.Add(chunkCoord);
                    SpawnJunkAtChunk(chunkCoord);
                    
                }
            }
        }
    }

    void SpawnJunkAtChunk(Vector2Int chunkCoord)
    {
        Vector2 clusterCenter = new Vector2(
            chunkCoord.x * chunkSize + chunkSize / 2f,
            chunkCoord.y * chunkSize + chunkSize / 2f
        );

        int JunkChance = Random.Range( 0, JunkChancemax);

        if (JunkChance == 1)
        {
            int ClusterjunkCount = Random.Range(junkPerClusterMin, junkPerClusterMax + 1); //random ammount

            for (int i = 0; i < ClusterjunkCount; i++)
            {
                Vector2 offset = Random.insideUnitCircle * clusterRadius; //ranodom pos
                Vector2 spawnPos = clusterCenter + offset;
                Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));//random rotation
                GameObject astroid = Instantiate(junkPrefab, spawnPos, rotation); //spawning and saving to temp
                astroid.name = $"Astroid_{JunkCount}"; // naming
                Astroid data = astroid.GetComponent<Astroid>();//getting the script inside it
                if (data != null) //updating data
                {
                    data.asteroidName = $"Astroid_{JunkCount}";
                    data.Pos = spawnPos;
                }                

                JunkCount++;
            }
        }
        else if (JunkChance < SmallJunkChance)
        {
            int ClusterjunkCount = Random.Range(1, 4); //random ammount between 1&3 inclusive

            for (int i = 0; i < ClusterjunkCount; i++)
            {
                Vector2 offset = Random.insideUnitCircle * clusterRadius; //ranodom pos
                Vector2 spawnPos = clusterCenter + offset;
                Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));//random rotation
                GameObject astroid = Instantiate(junkPrefab, spawnPos, rotation); //spawning and saving to temp
                astroid.name = $"Astroid_{JunkCount}"; // naming
                Astroid data = astroid.GetComponent<Astroid>();//getting the script inside it
                if (data != null) //updating data
                {
                    data.asteroidName = $"Astroid_{JunkCount}";
                    data.Pos = spawnPos;
                }

                JunkCount++;
            }
        }

        
    }
}