/* File Name: TowerButton.cs
 * Author: Zibing Xin
 * Student Number: 301427981
 * 
 * Description:
 * Manage the tower build button.
 * 
 */

using UnityEngine;
using UnityEngine.UI;

namespace DoomsDayDefense
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private int towerIndex;
        [SerializeField] private CrystalType crystalType;

        [SerializeField] private GameObject TowerBuild;
        [SerializeField] private GameObject CancelBuild;

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
            // change button color when it is not interactable
            if (!GetComponent<Button>().interactable)
            {
                GetComponent<Image>().color = Color.gray;
            }
            else
            {
                GetComponent<Image>().color = Color.white;
            }
        }

        public void OnClick()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            BuildManager.Instance.SelectTower(towerIndex);

            TowerBuild.SetActive(false);
            CancelBuild.SetActive(true);
        }

        public void OnCancel()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            BuildManager.Instance.ClearSelection();
            TowerBuild.SetActive(true);
            CancelBuild.SetActive(false);
        }

        public void SelectArcherTower() => BuildManager.Instance.SelectTower(0);
        public void SelectCatapultTower() => BuildManager.Instance.SelectTower(1);
        public void SelectMechineGunTower() => BuildManager.Instance.SelectTower(2);
        public void SelectCannonTower() => BuildManager.Instance.SelectTower(3);

    }
}
