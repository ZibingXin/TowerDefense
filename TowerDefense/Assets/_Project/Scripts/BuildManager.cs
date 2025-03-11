using System.Collections.Generic;
using UnityEngine;

namespace DoomsDayDefense
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;

        [SerializeField] private GameObject[] towers;
        private GameObject selectedTower;
        public List<BuildSite> activeSites = new();

        public bool IsBuilding => selectedTower != null;

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
            if (configIndex < 0 || configIndex >= towers.Length) return;

            selectedTower = towers[configIndex];
            ToggleBuildSitesHighlight(true);
        }

        public void BuildTowerAt(BuildSite site)
        {
            if (selectedTower == null) return;

            TowerBase towerBasePrefab = selectedTower.GetComponent<TowerBase>();
            if (towerBasePrefab == null)
            {
                Debug.Log("Prefab is null");
                return;
            }

            // initial new tower
            Vector3 position = new(site.transform.position.x, site.transform.position.y + 0.2f, site.transform.position.z);
            GameObject newTower = Instantiate(selectedTower, position, Quaternion.identity);
            
            TowerBase towerInstance = newTower.GetComponent<TowerBase>();
            if (towerInstance == null)
            {
                Debug.Log("Tower instance is null");
                return;
            }

            // occupy the build site
            site.OccupySite(towerInstance);

            // get the crystal type and cost
            CrystalType crystalType = towerBasePrefab.crystalType;
            int cost = towerBasePrefab.buildCost;

            // deduct the crystal cost
            switch (crystalType)
            {
                case CrystalType.Red:
                    GameManager.Instance.redCrystals -= cost;
                    break;
                case CrystalType.Blue:
                    GameManager.Instance.blueCrystals -= cost;
                    break;
                case CrystalType.Green:
                    GameManager.Instance.greenCrystals -= cost;
                    break;
            }

            Debug.Log("Tower built at " + site.transform.position);

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
            selectedTower = null;
            ToggleBuildSitesHighlight(false);
        }

        
    }
}
