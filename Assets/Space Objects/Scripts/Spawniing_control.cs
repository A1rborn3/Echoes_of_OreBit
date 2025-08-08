using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

public class SpawningControl : MonoBehaviour
{
    public GameObject junkPrefab;
    public Transform player;

    //for the spawning system im going to be breaking the world into chunks

    [Header("Cluster Settings")] // headers to make it easier to controll in unity
    public int junkPerClusterMin = 5;
    public int junkPerClusterMax = 15;
    public float clusterRadius = 40f; //must be kept below half of chunk size to avoid overlap

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
                    SpawnClusterAtChunk(chunkCoord);
                }
            }
        }
    }

    void SpawnClusterAtChunk(Vector2Int chunkCoord)
    {
        Vector2 clusterCenter = new Vector2(
            chunkCoord.x * chunkSize + chunkSize / 2f,
            chunkCoord.y * chunkSize + chunkSize / 2f
        );

        int junkCount = Random.Range(junkPerClusterMin, junkPerClusterMax + 1); //random ammount

        for (int i = 0; i < junkCount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * clusterRadius; //ranodom pos
            Vector2 spawnPos = clusterCenter + offset; 
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));//random rotation
            Instantiate(junkPrefab, spawnPos, rotation);
        }
    }
}