using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DoomsDayDefense
{
    public class TowerUI : MonoBehaviour
    {
        public static TowerUI Instance;

        [Header("UI Elements")]
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private TextMeshProUGUI statsText;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button sellButton;

        private TowerBase currentTower;

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

        public void ShowTowerInfo(TowerBase tower)
        {
            currentTower = tower;
            infoPanel.SetActive(true);
            UpdateUI();
        }

        private void UpdateUI()
        {
            statsText.text = currentTower.TowerStats;
            sellButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Sell ({currentTower.SellValue})";
        }

        //public void OnUpgradeClicked()
        //{
        //    currentTower.UpgradeTower();
        //    UpdateUI();
        //}

        public void OnSellClicked()
        {
            currentTower.SellTower();
            infoPanel.SetActive(false);
        }

        public void OnClosePanel()
        {
            infoPanel.SetActive(false);
        }
    }

}
