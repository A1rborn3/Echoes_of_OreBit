using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fragments.Runtime
{
    [Serializable]
    public class ResourceInfo
    {
        [Header("Identity")]
        public string id;            // e.g. "Stone", "Gold", "AlienTech"
        public string displayName;   // just displayed name

        [Header("Tier + Chance")]
        public ResourceTier tier;
        [Range(0, 100)] public float weight = 10f;  // relative chance used for drop tables 0 to 100% 

        [Header("Prefab")]
        public GameObject prefab;    // Drag from: Space Fragments/Prefabs
    }

    public class ResourceDB : MonoBehaviour
    {
        public List<ResourceInfo> resources = new List<ResourceInfo>();

        public ResourceInfo GetById(string id)
        {
            return resources.Find(r => r.id == id);
        }

        public ResourceInfo[] All => resources.ToArray();
    }
}
