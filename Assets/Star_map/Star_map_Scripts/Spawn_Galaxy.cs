using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Spawn_Galaxy : MonoBehaviour
{
    public GameObject starPrefab;
    public Transform homeStar;
    public GameObject Highlight;
    public int minStarsPerRing = 4;
    public int maxStarsPerRing = 8;
    public float baseRingSpacing = 100f;
    public int ringCount = 5; //maybe change when upgraded
    public float minStarSpacing = 50f;
    private List<Vector2> starPositions = new List<Vector2>();
    int seed = 1; //possible option to change seed in menu. save seed in files
   

    void Start()
    {
        // testing line 
        seed = 100;
        //testing line
        if (seed == 1) //default meaning new save, save should update seed before this line
        {
            seed = Random.Range(100, 999); //ensuring a 3 digit seed, this will make things easier late prob
        }
        Random.InitState(seed);
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

                int attempts = 0;
                const int maxAttempts = 100;

                while (!IsPositionValid(position) && attempts < maxAttempts)
                {
                    angle = Random.Range(0f, Mathf.PI * 2f); // randomize position
                    offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                    position = (Vector2)homeStar.position + offset;
                    //generate new position
                    attempts++;
                }

                starPositions.Add(position);

                GameObject star = Instantiate(starPrefab, position, Quaternion.identity);
                star.name = $"Star_Ring{ring}_#{i}";

                System_data data = star.GetComponent<System_data>();
                if (data != null)
                {
                    data.System_Ring = ring;
                    data.System_name = star.name;
                    string seedString = $"{ring:D2}{i:D2}{seed}"; //star seeds, format, ring 00, star 00, seed 000. 1205100 is ring 12 star 5 seed 100
                    data.Seed = int.Parse(seedString);
                    data.Star_pos = position;
                }
                else
                {
                    Debug.LogWarning("System_data component not found on star prefab!");
                }
                //running the distance update once all stars are loaded
                Distance_Update();
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


    //the following code controls the highlight to see current star system
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "Star_map")
        {
            string starName = Data_Transfer.Star_name;
            GameObject currentStar = GameObject.Find(starName);
            if (currentStar != null && Highlight != null)
            {
                // Move highlight to star position
                Highlight.transform.position = currentStar.transform.position;
                Highlight.transform.position += Vector3.back * 0.1f; //moving behind star

                Highlight.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Star or highlight not found!"); //this will always run on first launch. ill code it out later
                Highlight.SetActive(false); // hide if no star
            }
            //calculate and assign distances
            Distance_Update();



        }
    }

    private void Distance_Update()
    {
        string starName = Data_Transfer.Star_name;
        GameObject currentStar = GameObject.Find(starName);
        System_data currentData = null;
        if (currentStar != null)
        {
            currentData = currentStar.GetComponent<System_data>();
        }

        Vector2 playerPos; //finding most rencent star or home base

        if (currentData != null)
        {
            playerPos = currentData.Star_pos;
        }
        else if (homeStar != null)
        {
            playerPos = homeStar.position;
        }
        else { playerPos = Vector3.zero; } //setting to 0 if somethings broken


        System_data[] allStars = FindObjectsByType<System_data>(FindObjectsSortMode.None);
        Debug.Log("fired ");
        foreach (System_data star in allStars) //for every star edit data to shows distance from player
        {
            star.DistanceFromPlayer = Vector2.Distance(playerPos, star.Star_pos);
            Debug.Log(star.System_name + " distance: " + star.DistanceFromPlayer);
        }
    }

}
