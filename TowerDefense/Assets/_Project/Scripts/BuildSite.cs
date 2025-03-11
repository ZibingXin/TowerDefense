using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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

        //public void OnMouseEnter()
        //{
        //    //SetHightlight(true);
        //    GetComponent<MeshRenderer>().material = highlightMaterial;
        //}

        public void SetHightlight(bool state)
        {
            if (!IsOccupied)
            {
                GetComponent<MeshRenderer>().material = state? highlightMaterial : startMaterial;
            }
        }

        private void OnMouseDown()
        {
            //if (EventSystem.current.IsPointerOverGameObject()) return;

            if (!IsOccupied && BuildManager.Instance.IsBuilding)
            {
                BuildManager.Instance.BuildTowerAt(this);
            }
            else if (IsOccupied)
            {
                //TowerUI.Instance.ShowTowerInfo(currentTower);
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
