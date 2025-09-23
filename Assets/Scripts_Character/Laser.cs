using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 20f;// all upgradable
    public int damage = 25;
    public float lifetime = 2f; // destroy after 2s, can be changed with upgrades but ensures it doesnt exist forever
    public float destroyTimer = 0.1f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed; //shoot out the front
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision) //asseses the collision of the laser
    {
        Debug.Log("hit detected");
        Astroid asteroid = collision.GetComponent<Astroid>();
        if (asteroid != null)
        {
            asteroid.TakeDamage(damage);
            Destroy(gameObject, destroyTimer);
        }
    }

}
