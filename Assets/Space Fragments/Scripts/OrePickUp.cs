using UnityEngine;

namespace Fragments.Runtime
{
    [RequireComponent(typeof(Collider2D))]
    public class OrePickup : MonoBehaviour
    {
        [Header("Optional payload (use prefab name if blank)")]
        public string resourceIdOverride = "";
        public int amount = 1;

        [Header("FX")]
        public float rotateSpeed = 45f;

        void Reset()
        {
            var col = GetComponent<Collider2D>();
            col.isTrigger = true;
        }

        void Update()
        {
            transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var inv = other.GetComponentInParent<IInventory>();
            if (inv == null) return;

            string id = string.IsNullOrWhiteSpace(resourceIdOverride) ? gameObject.name : resourceIdOverride;
            if (inv.TryAdd(id, amount))
                Destroy(gameObject);
        }
    }
}
