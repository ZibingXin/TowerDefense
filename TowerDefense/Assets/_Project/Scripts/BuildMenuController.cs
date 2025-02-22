using UnityEngine;

namespace DoomsDayDefense
{
    public class BuildMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private Transform buttonsParent;

        void Start()
        {
            InitializeButtons();
        }

        void InitializeButtons()
        {
            foreach (GameObject tower in BuildManager.Instance.availableTowers)
            {
                TowerBlueprint blueprint = tower.GetComponent<TowerBlueprint>();
                GameObject buttonObj = Instantiate(buttonPrefab, buttonsParent);
                BuildButton button = buttonObj.GetComponent<BuildButton>();

                button.Initialize(
                    blueprint.icon,
                    blueprint.towerName,
                    blueprint.cost.ToString(),
                    () => BuildManager.Instance.ActiveSite.BuildTower(tower)
                );
            }
        }

    }
}
