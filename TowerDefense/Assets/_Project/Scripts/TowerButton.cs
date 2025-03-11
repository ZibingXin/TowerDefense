using UnityEngine;
using UnityEngine.UI;

namespace DoomsDayDefense
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private int towerIndex;
        [SerializeField] private CrystalType crystalType;

        private void Update()
        {
            switch (crystalType)
            {
                case CrystalType.Red:
                    GetComponent<Button>().interactable = GameManager.Instance.redCrystals >= 50;
                    break;
                case CrystalType.Green:
                    GetComponent<Button>().interactable = GameManager.Instance.greenCrystals >= 50;
                    break;
                case CrystalType.Blue:
                    GetComponent<Button>().interactable = GameManager.Instance.blueCrystals >= 50;
                    break;
            }
            //GetComponent<Button>().interactable = GameManager.Instance.currentGold >= 50;
        }

        public void OnClick()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            BuildManager.Instance.SelectTower(towerIndex);
        }

        public void SelectArcherTower() => BuildManager.Instance.SelectTower(0);
        public void SelectCatapultTower() => BuildManager.Instance.SelectTower(1);

    }
}
