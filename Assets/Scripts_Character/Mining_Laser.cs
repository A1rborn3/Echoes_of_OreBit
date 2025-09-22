using UnityEngine;

public class Mining_Laser : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform Player;

    public float fireRate = 0.5f; //seconds between shots
    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //add a constant fire rate later
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; //set next allowed fire timer
        }
    }

    void Shoot()
    {
        Instantiate(laserPrefab, Player.position, Player.rotation);
    }
}
