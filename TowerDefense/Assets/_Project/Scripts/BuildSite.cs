/* File Name: BuildSite.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Manage the build site for the tower.
 * 
 */

using UnityEngine;

namespace DoomsDayDefense
{
    public class BuildSite : MonoBehaviour
    {
        [SerializeField] private Material highlightMaterial;
        private Material startMaterial;

        private TowerBase currentTower;

        public bool IsOccupied => currentTower != null;

        private void Start()
        {
            BuildManager.Instance.RegisterBuildSite(this);
            startMaterial = GetComponent<MeshRenderer>().material;

        }

        private void OnDestroy()
        {
            BuildManager.Instance?.UnregisterBuildSite(this);
        }

        public void SetHightlight(bool state)
        {
            if (!IsOccupied)
            {
                GetComponent<MeshRenderer>().material = state? highlightMaterial : startMaterial;
            }
        }

        private void OnMouseDown()
        {
            if (!IsOccupied && BuildManager.Instance.IsBuilding)
            {
                BuildManager.Instance.BuildTowerAt(this);
            }
            else
            {
                return;
            }
        }

        public void OccupySite(TowerBase tower)
        {
            currentTower = tower;
            tower.OnDestroyed += ClearSite;
            SetHightlight(false);
        }

        private void ClearSite()
        {
            currentTower = null;
        }
    }
}
