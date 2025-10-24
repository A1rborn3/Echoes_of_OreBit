using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Fragments.Runtime
{
    [RequireComponent(typeof(Astroid))]
    public class DropOnDestroy2D : MonoBehaviour
    {
        [Header("Refs")]
        public ResourceDB db;

        [Header("Drop Rolls")]
        public int rolls = 3;               // how many ores spawns
        public float scatterRadius = 0.6f;  //radius after destoryed
        public float impulse = 2f;

        Astroid _asteroid;

        [System.Obsolete]
        void Awake()
        {
            _asteroid = GetComponent<Astroid>();

            if (_asteroid == null)
            {
                Debug.LogError("No Astroid component found!");
                return;
            }

            // Automatically find the ResourceDB if not assigned
            if (db == null)
            {
                db = FindObjectOfType<ResourceDB>();
                if (db == null)
                    Debug.LogWarning("No ResourceDB found in scene. Asteroid will not drop resources!");
            }

            _asteroid.OnDied.AddListener(SpawnDropsAndDestroy);
        }

        void SpawnDropsAndDestroy()
        {
            Debug.Log("invoked spawns");
            if (db != null)
            {
                var chosen = RollResources(db.All, rolls);
                foreach (var res in chosen)
                    SpawnPickup(res.prefab);
            }
        }

        List<ResourceInfo> RollResources(ResourceInfo[] all, int count)
        {
            var result = new List<ResourceInfo>();
            if (all == null || all.Length == 0 || count <= 0) return result;

            float total = 0f;
            foreach (var r in all) total += Mathf.Max(0f, r.weight);
            if (total <= 0f) return result;

            for (int i = 0; i < count; i++)
            {
                float r = Random.value * total;
                float acc = 0f;
                foreach (var res in all)
                {
                    acc += Mathf.Max(0f, res.weight);
                    if (r <= acc)
                    {
                        result.Add(res);
                        break;
                    }
                }
            }
            return result;
        }

        void SpawnPickup(GameObject prefab)
        {
            if (prefab == null) return;

            var go = Instantiate(prefab, transform.position, Quaternion.identity);

            // ensure OrePickup exists
            var pickup = go.GetComponent<OrePickup>();
            if (!pickup) pickup = go.AddComponent<OrePickup>();

            var rb = go.GetComponent<Rigidbody2D>();
            if (rb)
            {
                Vector2 offset = Random.insideUnitCircle * scatterRadius;
                go.transform.position += (Vector3)offset;
                rb.AddForce(Random.insideUnitCircle.normalized * impulse, ForceMode2D.Impulse);
            }
        }
    }
}
