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
            // Only trigger when the player touches it (optional but recommended)
            if (!other.CompareTag("Player")) return;

            // Use the singleton InventoryManager
            var inv = InventoryManager.Instance;
            if (inv == null)
            {
                Debug.LogWarning("No InventoryManager instance found in scene!");
                return;
            }

            // Determine item ID (either override or prefab name)
            string id = string.IsNullOrWhiteSpace(resourceIdOverride) ? gameObject.name : resourceIdOverride;

            // Add item and destroy pickup
            inv.AddItem(id, amount);
            Destroy(gameObject);
        }
    }
}