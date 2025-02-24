using UnityEngine;

namespace DoomsDayDefense
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private int towerIndex;

        public void OnClick()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", Vector3.zero);
            BuildManager.Instance.SelectTower(towerIndex);
        }

        public void SelectArcherTower() => BuildManager.Instance.SelectTower(0);
        public void SelectCatapultTower() => BuildManager.Instance.SelectTower(1);

    }
}
