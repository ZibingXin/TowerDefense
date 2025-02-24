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

            Vector3 position = new(site.transform.position.x, site.transform.position.y + 0.2f, site.transform.position.z);

            // create a new tower on the base site

            selectedTower = Instantiate(selectedTower, position, Quaternion.identity);

            //TowerBase newTower = selectedTower.GetComponent<TowerBase>();
            //newTower.transform.SetPositionAndRotation(position, Quaternion.identity);
            ////newTower.transform.parent = site.transform;
            //newTower.gameObject.SetActive(true);


            //TowerBase newTower = Instantiate(
            //    selectedTower,
            //    position,
            //    Quaternion.identity
            //);

            //newTower.InitializeTower();
            site.OccupySite(selectedTower.GetComponent<TowerBase>());

            GameManager.Instance.currentGold -= selectedTower.GetComponent<TowerBase>().buildCost;

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
