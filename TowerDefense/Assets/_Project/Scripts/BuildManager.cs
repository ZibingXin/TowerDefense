using System.Collections.Generic;
using UnityEngine;

namespace DoomsDayDefense
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;

        [System.Serializable]
        public class TowerPrefabConfig
        {
            public string towerName;
            public TowerBase towerPrefab;
            public Sprite buttonIcon;
            public int buildCost;
        }

        [SerializeField] private TowerPrefabConfig[] towerConfigs;
        private TowerPrefabConfig selectedTowerConfig;
        private List<BuildSite> activeSites = new List<BuildSite>();

        public bool IsBuilding => selectedTowerConfig != null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                ClearSelection();
            }
        }

        public void SelectTower(int configIndex)
        {
            if (configIndex < 0 || configIndex >= towerConfigs.Length) return;

            selectedTowerConfig = towerConfigs[configIndex];
            ToggleBuildSitesHighlight(true);
        }

        public void BuildTowerAt(BuildSite site)
        {
            if (selectedTowerConfig == null) return;

            TowerBase newTower = Instantiate(
                selectedTowerConfig.towerPrefab,
                site.transform.position,
                Quaternion.identity
            );

            newTower.InitializeTower();
            site.OccupySite(newTower);

            ClearSelection();
        }

        public void RegisterBuildSite(BuildSite site)
        {
            activeSites.Add(site);
        }

        public void UnregisterBuildSite(BuildSite site)
        {
            activeSites.Remove(site);
        }

        private void ToggleBuildSitesHighlight(bool state)
        {
            foreach (var site in activeSites)
            {
                site.SetHightlight(state && !site.IsOccupied);
            }
        }

        private void ClearSelection()
        {
            selectedTowerConfig = null;
            ToggleBuildSitesHighlight(false);
        }

        
    }
}
