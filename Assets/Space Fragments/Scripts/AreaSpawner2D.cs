using UnityEngine;
using Random = UnityEngine.Random;

namespace Fragments.Runtime
{
    public class AreaSpawner2D : MonoBehaviour
    {
        [Header("Area (BoxCollider2D bounds)")]
        public BoxCollider2D area;

        [Header("Prefabs to spawn")]
        public GameObject[] orePrefabs;

        public int spawnAmount = 10;

        [ContextMenu("Spawn Now")]
        public void SpawnNow()
        {
            if (!area || orePrefabs == null || orePrefabs.Length == 0)
            {
                Debug.LogWarning("[AreaSpawner2D] Missing area or prefabs.");
                return;
            }

            for (int i = 0; i < spawnAmount; i++)
            {
                Vector2 pos = RandomPointInArea();
                int idx = Random.Range(0, orePrefabs.Length);
                Instantiate(orePrefabs[idx], pos, Quaternion.identity);
            }
        }

        Vector2 RandomPointInArea()
        {
            var b = area.bounds;
            float x = Random.Range(b.min.x, b.max.x);
            float y = Random.Range(b.min.y, b.max.y);
            return new Vector2(x, y);
        }
    }
}
