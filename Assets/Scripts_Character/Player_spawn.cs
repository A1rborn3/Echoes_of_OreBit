using UnityEngine;

public class Player_spawn : MonoBehaviour
{
    // I dont know why but when you load from main menu the player breaks, this is a fix for that, i suspect its because of the audio somehow

    public GameObject playerPrefab;
    private Vector2 spawnPosition = new Vector2(0.0f, 0.0f);
    void Start()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
        else Debug.Log("Player exists");
    }
    private void Update()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("player spawned");
        }
        else Debug.Log("Player exists");
    }

}
