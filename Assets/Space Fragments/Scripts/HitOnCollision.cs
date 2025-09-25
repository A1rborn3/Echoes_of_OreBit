using UnityEngine;
using Fragments.Runtime;

public class HitOnCollision : MonoBehaviour
{
    public float damage = 25f;
    public bool destroyOnHit = true;

    void OnCollisionEnter2D(Collision2D col)
    {
        var hp = col.collider.GetComponentInParent<Health2D>();
        if (hp != null)
        {
            hp.ApplyDamage(damage);
            if (destroyOnHit) Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var hp = other.GetComponentInParent<Health2D>();
        if (hp != null)
        {
            hp.ApplyDamage(damage);
            if (destroyOnHit) Destroy(gameObject);
        }
    }
}