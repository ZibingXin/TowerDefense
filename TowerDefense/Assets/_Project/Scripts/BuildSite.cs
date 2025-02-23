using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DoomsDayDefense
{
    public class BuildSite : MonoBehaviour
    {
        [SerializeField] private GameObject highlightEffect;
        private TowerBase currentTower;

        public bool IsOccupied => currentTower != null;

        private void Start()
        {
            BuildManager.Instance.RegisterBuildSite(this);
            highlightEffect.SetActive(false);
        }

        private void OnDestroy()
        {
            BuildManager.Instance?.UnregisterBuildSite(this);
        }

        public void SetHightlight(bool state)
        {
            if (!IsOccupied)
            {
                highlightEffect.SetActive(state);
            }
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (!IsOccupied && BuildManager.Instance.IsBuilding)
            {
                BuildManager.Instance.BuildTowerAt(this);
            }
            else if (IsOccupied)
            {
                TowerUI.Instance.ShowTowerInfo(currentTower);
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
