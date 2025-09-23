using UnityEngine;

public class Astroid : MonoBehaviour
{
    public string asteroidName;   //set up with spawn control
    public int maxHealth = 100;
    private int currentHealth;
    public Vector2 Pos;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Die();
        }
        
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
