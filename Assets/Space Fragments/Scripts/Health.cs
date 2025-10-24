using UnityEngine;
using UnityEngine.Events;

namespace Fragments.Runtime
{
    [DisallowMultipleComponent]
    public class Health2D : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float currentHealth = 100f;

        [Header("Testing")]
        public bool drainOverTime = true;
        public float drainPerSecond = 10f;

        [System.Serializable] public class DeathEvent : UnityEvent { }
        public DeathEvent OnDied;

        bool _dead;

        void Reset() => currentHealth = maxHealth;

        void Update()
        {
            if (_dead) return;
            if (drainOverTime)
            {
                currentHealth -= drainPerSecond * Time.deltaTime;
                if (currentHealth <= 0f) Die();
            }
        }

        public void ApplyDamage(float dmg)
        {
            if (_dead) return;
            currentHealth -= Mathf.Max(0f, dmg);
            if (currentHealth <= 0f) Die();
        }

        void Die()
        {
            if (_dead) return;
            _dead = true;
            OnDied?.Invoke();
        }
    }
}
