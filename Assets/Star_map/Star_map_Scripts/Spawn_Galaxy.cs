using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Spawn_Galaxy : MonoBehaviour
{
    public GameObject starPrefab;
    public Transform homeStar;
    public int minStarsPerRing = 4;
    public int maxStarsPerRing = 8;
    public float baseRingSpacing = 100f;
    public int ringCount = 5; //maybe change when upgraded
    public float minStarSpacing = 10f;
    private List<Vector2> starPositions = new List<Vector2>();

    void Start()
    {
        if (homeStar == null)
        {
            Debug.LogWarning("HomeStar not set! Creating temporary at origin.");
            GameObject tempHome = new GameObject("HomeStar");
            tempHome.transform.position = Vector3.zero;
            homeStar = tempHome.transform;
        }

        for (int ring = 1; ring <= ringCount; ring++)
        {
            int starCount = Random.Range(minStarsPerRing, maxStarsPerRing + 1);
            float radius = baseRingSpacing * ring;

            for (int i = 0; i < starCount; i++)
            {
                float angle = Random.Range(0f, Mathf.PI * 2f); // randomize position
                Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                Vector2 position = (Vector2)homeStar.position + offset;

                while (!IsPositionValid(position))
                {
                    angle = Random.Range(0f, Mathf.PI * 2f); // randomize position
                    offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                    position = (Vector2)homeStar.position + offset;
                    //generate new position
                }

                starPositions.Add(position);

                GameObject star = Instantiate(starPrefab, position, Quaternion.identity);
                star.name = $"Star_Ring{ring}_#{i}";

                System_data data = star.GetComponent<System_data>();
                if (data != null)
                {
                    data.System_Ring = ring;
                    data.System_name = star.name;
                }
                else
                {
                    Debug.LogWarning("System_data component not found on star prefab!");
                }

                //seed transfer when i figure that out

                

            }
        }
    }

    bool IsPositionValid(Vector2 pos)
    {
        foreach (Vector2 existing in starPositions)
        {
            if (Vector2.Distance(pos, existing) < minStarSpacing)
            {
                return false; // too close
            }
        }
        return true;
    }
}
