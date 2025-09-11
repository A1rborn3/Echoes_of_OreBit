using UnityEngine;
using System.Collections.Generic;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject starPrefab;
    public GameObject[] planetPrefabs;
    public int minPlanets = 3;
    public int maxPlanets = 5;
    public float minSpawnRadius = 1000f; // inner edge of donut
    public float maxSpawnRadius = 3000f; // outer edge of donut
    public float minDistanceBetweenPlanets = 1000f;
    public float spawnBuffer = 500f;
    public Transform player;
    private Vector2 starPosition;
    public int SystemRing = Data_Transfer.System_ring; // this is what wil determand the reasorces // ie 1= shit reasorces 2 = less shit 3 = moderate ect ect
    

    private List<Vector2> planetPositions = new List<Vector2>();

    void Start()
    {
        int SystemSeed = Data_Transfer.current_star;
        Debug.Log("System seed = " + SystemSeed);
        Random.InitState(SystemSeed);
        SpawnStar();
        SpawnPlanets();
        
    }

    void SpawnPlanets()
    {
        int planetCount = Random.Range(minPlanets, maxPlanets + 1);

        for (int i = 0; i < planetCount; i++)
        {
            Vector2 position = FindValidPosition();
            if (position != Vector2.zero)
            {
                planetPositions.Add(position);
                GameObject prefab = planetPrefabs[Random.Range(0, planetPrefabs.Length)];
                Instantiate(prefab, position, Quaternion.identity);
            }
        }
    }

    Vector2 FindValidPosition()
    {
        int attempts = 50;
        while (attempts-- > 0)
        {
            float distance = Random.Range(minSpawnRadius, maxSpawnRadius);
            Vector2 offset = Random.insideUnitCircle.normalized * distance;
            Vector2 candidate = starPosition + offset; //centered on star

            bool valid = true;
            foreach (Vector2 existing in planetPositions)
            {
                if (Vector2.Distance(existing, candidate) < minDistanceBetweenPlanets)
                {
                    valid = false;
                    break;
                }
            }

            if (valid) return candidate;
        }
        return Vector2.zero;
    }
    void SpawnStar()
    {
        // Pick a random direction
        float angle = Random.Range(0f, Mathf.PI * 2f);
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)); // 0 - 360 in radons 

        // Place the star maxPlanetDistance + buffer away from the player so player starts outside of planets
        starPosition = (Vector2)player.position + direction * (maxSpawnRadius + spawnBuffer);

        Instantiate(starPrefab, starPosition, Quaternion.identity);
    }

}