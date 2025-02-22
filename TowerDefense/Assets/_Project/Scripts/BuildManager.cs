using System.Collections.Generic;
using UnityEngine;

namespace DoomsDayDefense
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;

        [Header("Tower Blueprints")]
        public List<GameObject> availableTowers = new List<GameObject>();

        public BuildSite ActiveSite { get; private set; }
        public bool IsBuilding { get; private set; }

        void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void SetActiveSite(BuildSite site)
        {
            ActiveSite = site;
            IsBuilding = true;
        }

        public void CompleteBuilding()
        {
            ActiveSite = null;
            IsBuilding = false;
        }

        public GameObject GetTowerPrefab(int index)
        {
            if (index >= 0 && index < availableTowers.Count)
                return availableTowers[index];
            return null;
        }
    }
}
