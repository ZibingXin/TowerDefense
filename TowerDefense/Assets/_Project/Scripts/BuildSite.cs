using UnityEngine;

namespace DoomsDayDefense
{
    public class BuildSite : MonoBehaviour
    {
        [Header("Settings")]
        public bool isOccupied = false;
        public Vector3 towerOffset = new Vector3(0, 0.2f, 0);

        [Header("Visuals")]
        public Material highlightMaterial;
        private Material originalMaterial;
        private MeshRenderer meshRenderer;

        [Header("UI")]
        public Canvas buildMenuCanvas;
        public RectTransform buttonPanel;

        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            originalMaterial = meshRenderer.material;
            buildMenuCanvas.gameObject.SetActive(false);
        }

        void OnMouseEnter()
        {
            if (!isOccupied)
                meshRenderer.material = highlightMaterial;
        }

        void OnMouseExit()
        {
            meshRenderer.material = originalMaterial;
        }

        void OnMouseDown()
        {
            if (!isOccupied && !BuildManager.Instance.IsBuilding)
                ShowBuildMenu();
        }

        public void ShowBuildMenu()
        {
            BuildManager.Instance.SetActiveSite(this);
            buildMenuCanvas.gameObject.SetActive(true);
            PositionBuildMenu();
        }

        void PositionBuildMenu()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            buttonPanel.position = screenPos + new Vector3(0, 100, 0);
        }

        public void BuildTower(GameObject towerPrefab)
        {
            if (isOccupied) return;

            TowerBlueprint blueprint = towerPrefab.GetComponent<TowerBlueprint>();
            if (GameManager.Instance.PurchaseTower(blueprint.cost))
            {
                Instantiate(towerPrefab, transform.position + towerOffset, Quaternion.identity);
                isOccupied = true;
                buildMenuCanvas.gameObject.SetActive(false);
                BuildManager.Instance.CompleteBuilding();
            }
            else
            {
                // gold not enough
                //UIManager.Instance.ShowMessage("Gold not enough");
            }
        }
    }
}
