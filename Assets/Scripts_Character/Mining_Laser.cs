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
        GameObject laser = Instantiate(laserPrefab, Player.position, Player.rotation);

        AudioManager.Instance?.PlaySFX("laser_shoot");

        Collider2D playerCollider = Player.GetComponent<Collider2D>();
        Collider2D[] laserCollider = laser.GetComponentsInChildren<Collider2D>();

        //Ignore collision between the ship collider and the laser collider
        
        foreach (var lc in laserCollider) if (lc != null && playerCollider != null)
            {
                Physics2D.IgnoreCollision(lc, playerCollider);
                lc.enabled = true;

            }
    }
}

